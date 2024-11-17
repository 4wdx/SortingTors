using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Root.Services;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    public class Pin : MonoBehaviour
    {
        public event Action<bool> OnFillStateChange;
        
        [SerializeField] private Torus[] _tors;
        
        private Torus[] _dragableObjects;
        private Vector3[] _startPositions;
        private int _currentCount;
        private int _maxCount;

        private void Awake()
        {
            _maxCount = Const.TorsInPin;
            _startPositions = new Vector3[_maxCount];
            _dragableObjects = new Torus[_maxCount];

            for (int i = 0; i < _tors.Length; i++) 
                _startPositions[i] = _tors[i].transform.position;
        }

        public void Initialize(PinData pinData)
        {
            for (int i = 0; i < _dragableObjects.Length; i++) 
                _dragableObjects[i] = null;
            
            _currentCount = 0;
            for (int i = 0; i < pinData.TorsColors.Length; i++)
            {
                if (pinData.TorsColors[i] > 0)
                {
                    _tors[i].gameObject.SetActive(true);
                    _tors[i].transform.position = _startPositions[i];
                    _tors[i].Initialize(pinData.TorsColors[i]);
                    
                    Debug.Log("pin: " + gameObject.name + ", tor: " + _tors[i].name + ", color: " + pinData.TorsColors[i]);
                    Add(_tors[i]);
                }
                else 
                    _tors[i].gameObject.SetActive(false);
            }
        }

        public void Hide()
        {
            _currentCount = 0;
            for (int i = 0; i < _dragableObjects.Length; i++) 
                _dragableObjects[i] = null;
            
            foreach (Torus tor in _tors)
                tor.gameObject.SetActive(false);
        }

        public Torus GetUpperTorus()
        {
            if (_currentCount == 0) 
                return null;
            
            return GetUpper();
        }

        public Vector3 GetUpperPosition()
        {
            return _startPositions[_currentCount - 1];
        }

        public void RemoveUpperTorus()
        {

            if (_currentCount == _maxCount && CheckFillState())
            {
                OnFillStateChange?.Invoke(false);
            }
            Remove();
        }

        public bool CanSet()
        {
            if (_currentCount == _maxCount)
                return false;

            return true;
        }

        public void SetDragable(Torus draggable)
        {
            Add(draggable);
            draggable.StopDrag(_startPositions[_currentCount - 1]);

            if (_currentCount == _maxCount && CheckFillState())
            {
                OnFillStateChange?.Invoke(true);
            }
        }

        private void Add(Torus tor)
        {
            _currentCount++;
            _dragableObjects[_currentCount - 1] = tor;
        }

        private Torus GetUpper()
        {
            return _dragableObjects[_currentCount - 1];
        }
        
        private void Remove()
        {
            _dragableObjects[_currentCount - 1] = null;
            _currentCount--;
        }
        
        private bool CheckFillState()
        {
            int parentColor = _dragableObjects[0].Color;
            return _dragableObjects.All(torus => torus.Color == parentColor);
        }
    }
}