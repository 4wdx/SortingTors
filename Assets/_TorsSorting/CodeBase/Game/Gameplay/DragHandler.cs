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
            
            if (_cachedPin.TryGetUpperDragable(out _dragableTorus))
            {
                _mouseWorldPosition.position = hit.point;
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
                    if (pin.TrySetDragable(_dragableTorus))
                        return;
                }
            }
            
            _cachedPin.TrySetDragable(_dragableTorus);
            _dragableTorus.StopDrag();
            _dragableTorus = null;
        }

        private bool Raycast(LayerMask layerMask, out RaycastHit hit)
        {
            return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask);
        }
    }
}