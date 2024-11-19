using System;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    public class PinsStack : MonoBehaviour
    {
        public event Action OnComplete;
        
        [SerializeField] private Pin[] _mainPins;
        [SerializeField] private Pin[] _rewardedPins;
        [SerializeField] private int _colors;
        [SerializeField] private Transform _plane;
        private int _completedCount;
        private int _addedPinCount;
        private Mesh _torusMesh;

        public void Initialize(Mesh torusMesh)
        {
            _torusMesh = torusMesh;
            _completedCount = 0;
            
            foreach (Pin pin in _mainPins)
            {
                pin.Initialize(torusMesh, _plane.position.y);
                pin.OnCompleted += UpdateCompletedCounter;
            }

            foreach (Pin pin in _rewardedPins) 
                pin.gameObject.SetActive(false);
        }

        public void AddPin()
        {
            if (_addedPinCount > _rewardedPins.Length)
                return;
            
            _rewardedPins[_addedPinCount].gameObject.SetActive(true);
            _rewardedPins[_addedPinCount].Initialize(_torusMesh, _plane.position.y);
            _rewardedPins[_addedPinCount].OnCompleted += UpdateCompletedCounter;
            _addedPinCount++;
        }

        private void UpdateCompletedCounter()
        {
            _completedCount++;
            if (_completedCount == _colors)
                OnComplete?.Invoke();
        }

        private void OnDestroy()
        {
            foreach (Pin pin in _mainPins)
                pin.OnCompleted -= UpdateCompletedCounter;
        }
    }
}