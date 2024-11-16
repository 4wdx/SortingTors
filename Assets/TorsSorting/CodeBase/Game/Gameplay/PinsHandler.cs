using System;
using CodeBase.Root.Services;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    public class PinsHandler : MonoBehaviour
    {
        public event Action OnComplete;
        
        [SerializeField] private Pin[] _pins;
        private int _completedCount;
        
        private void Awake()
        {
            foreach (Pin pin in _pins) 
                pin.OnFillStateChange += UpdateCompletedCounter;
        }

        public void Initialize(PinData[] pinsData)
        {
            _completedCount = 1;
            
            for (int i = 0; i < _pins.Length; i++)
            {
                if (pinsData[i].Enabled == true)
                {
                    _pins[i].gameObject.SetActive(true);
                    _pins[i].Initialize(pinsData[i]);
                }
                else
                {
                    _pins[i].Hide();
                    _pins[i].gameObject.SetActive(false);
                    _completedCount++;
                }
            }
        }

        private void UpdateCompletedCounter(bool success)
        {
            if (success == true)
                _completedCount++;
            else 
                _completedCount--;
            
            print(_completedCount);
            if (_completedCount == _pins.Length)
                OnComplete?.Invoke();
        }

        private void OnDestroy()
        {
            foreach (Pin pin in _pins)
                pin.OnFillStateChange -= UpdateCompletedCounter;
        }
    }
}