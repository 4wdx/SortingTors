using System;
using CodeBase.Configs;
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
        private SkinData _skinData;

        public void StartGameplay()
        {
            _completedCount = 0;
            foreach (Pin pin in _mainPins)
            {
                pin.StartGameplay(_plane.position.y);
                pin.OnCompleted += CheckCompleted;
            }
        }
        
        public void SetView(SkinData skinData)
        {
            _skinData = skinData;
            
            foreach (Pin pin in _mainPins) 
                pin.SetSkin(_skinData);

            foreach (Pin pin in _rewardedPins) 
                pin.gameObject.SetActive(false);
        }

        public void AddPin()
        {
            if (_addedPinCount > _rewardedPins.Length)
                return;
            
            _rewardedPins[_addedPinCount].gameObject.SetActive(true);
            _rewardedPins[_addedPinCount].SetSkin(_skinData);
            _rewardedPins[_addedPinCount].StartGameplay(_plane.position.y);
            _rewardedPins[_addedPinCount].OnCompleted += CheckCompleted;
            
            _addedPinCount++;
        }

        private void CheckCompleted()
        {
            _completedCount++;
            if (_completedCount == _colors)
                Complete();
        }

        private void Complete()
        {
            foreach (Pin pin in _mainPins) 
                pin.PlayParticle();

            foreach (Pin pin in _rewardedPins)
            {
                if (pin.gameObject.activeInHierarchy)
                    pin.PlayParticle();
            }
            
            _completedCount = 0;
            OnComplete?.Invoke();
        }
    }
}