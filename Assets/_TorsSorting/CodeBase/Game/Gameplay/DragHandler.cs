using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    public class DragHandler : ITickable
    {
        public bool Enabled { get; set; }
        
        private readonly Transform _mouseWorldPosition;
        private Torus _dragableTorus;
        private Pin _cachedPin;
        private Vector3 _startPos;

        private readonly LayerMask _pinsLayer;
        private readonly LayerMask _planeLayer;

        public DragHandler(LayerMask pinsLayer, LayerMask planeLayer)
        {
            _pinsLayer = pinsLayer;
            _planeLayer = planeLayer;
            _mouseWorldPosition = new GameObject("MouseWorldPosition").transform;
        }
        
        public void Tick()
        {
            if (Enabled == false) return;
            
            if (Input.GetKeyDown(KeyCode.Mouse0)) OnStartHandle();

            if (Input.GetKey(KeyCode.Mouse0)) InProgressHandle();

            if (Input.GetKeyUp(KeyCode.Mouse0)) OnEndHandle();
        }

        private void OnStartHandle()
        {
            if (!Raycast(_pinsLayer, out RaycastHit hit)) return;
            
            if (hit.transform.TryGetComponent(out _cachedPin) == false)
                return;
            
            _dragableTorus = _cachedPin.GetUpperTorus();
            if (_dragableTorus != null)
            {
                _mouseWorldPosition.position = hit.point;
                _startPos = _cachedPin.GetUpperPosition();
                _dragableTorus.StartDrag(_mouseWorldPosition);
            }
        }

        private void InProgressHandle()
        {
            if (Raycast(_planeLayer, out RaycastHit hit)) 
                _mouseWorldPosition.position = hit.point;
        }

        private void OnEndHandle()
        {
            if (_dragableTorus == null) return;

            if (Raycast(_pinsLayer, out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent(out Pin pin))
                {
                    if (pin.CanSet() && pin != _cachedPin)
                    {
                        _cachedPin.RemoveUpperTorus();
                        pin.SetDragable(_dragableTorus);
                    }
                    else
                    {
                        _dragableTorus.StopDrag(_startPos);
                        _dragableTorus = null;
                        _cachedPin = null;
                    }
                }
                else
                {
                    _dragableTorus.StopDrag(_startPos);
                    _dragableTorus = null;
                    _cachedPin = null;
                }
            }
            else
            {
                _dragableTorus.StopDrag(_startPos);
                _dragableTorus = null;
                _cachedPin = null;
            }
        }

        private bool Raycast(LayerMask layerMask, out RaycastHit hit)
        {
            return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask);
        }
    }
}