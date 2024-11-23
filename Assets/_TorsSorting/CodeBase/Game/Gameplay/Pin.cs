using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Configs;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    public class Pin : MonoBehaviour
    {
        public event Action OnCompleted;
        
        [SerializeField] private Torus[] _tors;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private bool _playInAll;
        
        private Stack<Torus> _torusStack;
        private int _maxCount;
        private bool _isActive;
        
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

        public void StartGameplay(float upPos)
        {
            _isActive = true;
            _upPos = upPos;
        }
        
        public void SetSkin(SkinData skinData)
        {
            _torusStack.Clear();
            for (int i = 0; i < _tors.Length; i++)
            {
                _tors[i].transform.position = _startPositions[i];
                if (_tors[i].Color > 0)
                {
                    _torusStack.Push(_tors[i]);
                    _tors[i].SetSkin(skinData);
                    ;
                }
                else
                {
                    _tors[i].gameObject.SetActive(false);
                }
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
            if (_torusStack.TryPop(out returnedTorus) && _isActive)
            {
                returnedTorus.StartDrag(_upPos);
                return true;
            }
            
            returnedTorus = null;
            return false;
        }

        public void PlayParticle()
        {
            if (_playInAll)
            {
                _particleSystem.Play();
                return;
            }
            
            if (_isActive)
                _particleSystem.Play();
        }
        
        private void CheckComplete()
        {
            if (_torusStack.Count != _maxCount) return;
            
            int upperColor = _torusStack.Peek().Color; 
            if (_torusStack.Any(torus => torus.Color != upperColor))
            {
                return;
            }

            _isActive = false;
            _particleSystem.Play();
            OnCompleted?.Invoke();
        }
    }
}