using CodeBase.Game.Gameplay;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.PlayerInput
{
    public class DragHandler : ITickable
    {
        public bool Enabled { get; set; }
        
        private readonly Click<Pin> _click;
        private Torus _selectedTorus;
        private Pin _cachedPin;
        
        private Vector3 _startPos;
        
        public DragHandler(LayerMask pinsLayer, LayerMask planeLayer)
        {
            _click = new Click<Pin>(pinsLayer, planeLayer, 0.175f);
        }

        public void Tick(float deltaTime)
        {
            if (Enabled == false) return;
            
            if (Input.GetKeyDown(KeyCode.Mouse0)) OnStartHandle();

            _click.Tick(deltaTime);

            if (Input.GetKeyUp(KeyCode.Mouse0)) OnEndHandle();
        }

        private void OnStartHandle()
        {
            //if selected
            if (_selectedTorus)
            {
                //if has new pin
                Pin newPin = _click.StopClick();
                if (!newPin) return;
                
                if (newPin == _cachedPin)
                {
                    newPin.ReturnTorus(_selectedTorus);
                    ClearHandle();
                    return;
                }

                if (newPin.SetToUpper(_selectedTorus))
                {
                    ClearHandle();
                }
            }
            //new select
            else
            {
                _cachedPin = _click.StartClick();
                if (!_cachedPin) return;
            
                if (_cachedPin.RemoveUpperTorus(out _selectedTorus))
                    _click.OnDrag += Drag;
            }
        }
        
        private void Drag(Vector2 dragDelta)
        {
            if (_selectedTorus == null) return;
            
            Vector3 position = new(_click.MousePosition.x + dragDelta.x, _selectedTorus.transform.position.y, _click.MousePosition.y + dragDelta.y); ;
            _selectedTorus.transform.position = position;
        }

        private void OnEndHandle()
        {
            if (_selectedTorus == null) return;
            
            _click.OnDrag -= Drag;
            
            if (_click.Dragging) //if selected - put down
            {
                Pin newPin = _click.StopClick();
                if (newPin)
                {
                    if (newPin.SetToUpper(_selectedTorus))
                    {
                        ClearHandle();
                        return;
                    }
                }

                _cachedPin.ReturnTorus(_selectedTorus);
                ClearHandle();
            }
            else
                _click.StopClick();
        }

        private void ClearHandle()
        {
            _selectedTorus = null;
            _cachedPin = null;
        }
    }
}