using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Root.Services;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    public class Pin : MonoBehaviour
    {
        public event Action<bool> OnFillStateChange;
        
        [SerializeField] private Torus[] _tors;
        
        private List<Torus> _dragableObjects;
        private Vector3[] _startPositions;
        private int _maxCount;

        private void Awake()
        {
            _maxCount = _tors.Length;
            _startPositions = new Vector3[_maxCount];

            for (int i = 0; i < _tors.Length; i++) 
                _startPositions[i] = _tors[i].transform.position;
        }

        public void Initialize(PinData pinData)
        {
            _dragableObjects = new List<Torus>();
            for (int i = 0; i < pinData.TorsColors.Length; i++)
            {
                if (pinData.TorsColors[i] > 0)
                {
                    _tors[i].gameObject.SetActive(true);
                    _tors[i].transform.position = _startPositions[i];
                    
                    _tors[i].Initialize(pinData.TorsColors[i]);
                    print(pinData.TorsColors[i]);
                    _dragableObjects.Add(_tors[i]);
                }
                else 
                    _tors[i].gameObject.SetActive(false);
            }
        }

        public void Hide()
        {
            foreach (Torus tor in _tors)
                tor.gameObject.SetActive(false);
        }

        public bool TryGetUpperDragable(out Torus dragable)
        {
            if (_dragableObjects.Count == 0)
            {
                dragable = null;
                return false;
            }

            dragable = _dragableObjects.Last();
            if (dragable == null) 
                return false;

            if (_dragableObjects.Count == _maxCount && CheckFillState())
            {
                OnFillStateChange?.Invoke(false);
                print("remove");
            }
                
            _dragableObjects.Remove(dragable);
            return true;
        }

        public bool TrySetDragable(Torus draggable)
        {
            if (_dragableObjects.Count == _maxCount)
                return false;

            _dragableObjects.Add(draggable);
            draggable.StopDrag(_startPositions[_dragableObjects.Count - 1]);

            if (_dragableObjects.Count == _maxCount && CheckFillState())
            {
                OnFillStateChange?.Invoke(true);
                
                print("add");
            }
            
            return true;
        }

        private bool CheckFillState()
        {
            int parentColor = _dragableObjects[0].Color;
            return _dragableObjects.All(torus => torus.Color == parentColor);
        }
    }
}