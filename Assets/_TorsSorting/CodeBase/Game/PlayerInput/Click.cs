using System;
using CodeBase.Utils;
using UnityEditorInternal;
using UnityEngine;

namespace CodeBase.Game.PlayerInput
{
    public class Click<T> : ITickable where T : MonoBehaviour
    {
        public event Action<Vector2> OnDrag;

        public Vector2 MousePosition { get; private set; }

        private float LifeTime {get; set; }
        private bool Performed {get; set; }
        private T Component {get; set;}
        
        private readonly LayerMask _clickLayers;
        private readonly LayerMask _planeLayer;
        
        private readonly float _timeToDrag;
        private Vector2 _dragDelta;
        public bool Dragging { get; private set; }

        public Click(LayerMask clickLayers, LayerMask _planeLayer, float timeToDrag)
        {
            _clickLayers = clickLayers;
            this._planeLayer = _planeLayer;
            _timeToDrag = timeToDrag;
        }

        public T StartClick()
        {
            LifeTime = 0;
            Performed = true;
            
            if (!Raycast(_clickLayers, out RaycastHit hit)) 
                return null;
            
            Component = hit.collider.GetComponent<T>();
            return Component;
        }

        public void Tick(float deltaTime)
        {
            if (Performed == false) return;
            if (Component == null) return;
            
            LifeTime += deltaTime;
            
            Raycast(_planeLayer, out RaycastHit hit);
            Vector2 hitPos = new(hit.point.x, hit.point.z);
            MousePosition = hitPos;
            
            //start drag detect
            if (Dragging)
            {
                OnDrag?.Invoke(_dragDelta);
            }
            else
            {
                if (!(LifeTime >= _timeToDrag)) 
                    return;
                
                Vector2 objectPos = new(Component.transform.position.x, Component.transform.position.z);
                _dragDelta = objectPos - hitPos;
                
                Debug.Log("StartDrag");
                Dragging = true;
            }
        }
        
        public T StopClick()
        {
            /*if (Component != null)
            {
                T result = Raycast(_clickLayers, out RaycastHit hit, Component.transform.position) ? hit.collider.GetComponent<T>() : null;
                Performed = false;
                Dragging = false;
                Component = null;
                
                return result;
            }

            else
            {
                Performed = false;
                Dragging = false;
                Component = null;
                return Raycast(_clickLayers, out RaycastHit hit) ? hit.collider.GetComponent<T>() : null;
            }*/
            
            
            Performed = false;
            Component = null;
            Dragging = false;
            
            
            return Raycast(_clickLayers, out RaycastHit hit1) ? hit1.collider.GetComponent<T>() : null;
        } 
        
        private bool Raycast(LayerMask layerMask, out RaycastHit hit)
        {
            return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask);
        }

        private bool Raycast(LayerMask layerMask, out RaycastHit hit, Vector3 worldPosition)
        {
            return Physics.Raycast(Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(worldPosition)),
                out hit, Mathf.Infinity, layerMask);
        }
    }
}