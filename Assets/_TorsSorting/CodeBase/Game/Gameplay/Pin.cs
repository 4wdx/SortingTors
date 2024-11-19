using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    public class Pin : MonoBehaviour
    {
        public event Action OnCompleted;
        
        [SerializeField] private Torus[] _tors;
        
        private Stack<Torus> _torusStack;
        private int _maxCount;
        private bool _completed;
        
        private Vector3[] _startPositions;
        private float _upPos;

        private void Awake()
        {
            _maxCount = _tors.Length;
            _torusStack = new Stack<Torus>();

            _startPositions = new Vector3[_maxCount];
            for (int i = 0; i < _tors.Length; i++) 
                _startPositions[i] = _tors[i].transform.position;
        }

        public void Initialize(Mesh torusMesh, float upPos)
        {
            _torusStack.Clear();
            _upPos = upPos;
            
            for (int i = 0; i < _tors.Length; i++)
            {
                if (_tors[i].Color > 0)
                {
                    _tors[i].transform.position = _startPositions[i];
                    _tors[i].Initialize(torusMesh);
                    _torusStack.Push(_tors[i]);
                }
                else 
                    _tors[i].gameObject.SetActive(false);
            }
        }

        public bool SetToUpper(Torus addedTorus)
        {
            if (_torusStack.Count == _maxCount)
                return false;
            
            if (_torusStack.TryPeek(out Torus torus))
            {
                if(torus.Color != addedTorus.Color)
                    return false;
            }
            
            _torusStack.Push(addedTorus);
            print("push");
            addedTorus.StopDrag(_startPositions[_torusStack.Count - 1]);
            CheckComplete();
            return true;
        }

        public void ReturnTorus(Torus torus)
        {
            _torusStack.Push(torus);
            torus.StopDrag(_startPositions[_torusStack.Count - 1]);
        }

        public bool RemoveUpperTorus(out Torus returnedTorus)
        {
            if (!_completed && _torusStack.TryPop(out returnedTorus))
            {
                returnedTorus.StartDrag(_upPos);
                return true;
            }
            
            returnedTorus = null;
            return false;
        }
        
        private void CheckComplete()
        {
            if (_torusStack.Count != _maxCount) return;
            
            int upperColor = _torusStack.Peek().Color; 
            if (_torusStack.Any(torus => torus.Color != upperColor))
            {
                return;
            }

            _completed = true;
            OnCompleted?.Invoke();
        }
    }
}