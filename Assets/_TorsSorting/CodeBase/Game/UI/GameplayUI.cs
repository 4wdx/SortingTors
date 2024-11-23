using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.UI
{
    public class GameplayUI : MonoBehaviour
    {
        public event Action OnExit;
        public event Action OnAddPin;
        public event Action OnSkipLevel;
        
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _addPinButton;
        [SerializeField] private Button _skipLevelButton;
        
        public void Show()
        {
            _exitButton.onClick.AddListener(Exit);
            _addPinButton.onClick.AddListener(AddPin);
            _skipLevelButton.onClick.AddListener(SkipLevel);
        }

        public void Hide()
        {
            _exitButton.onClick.RemoveListener(Exit);
            _addPinButton.onClick.RemoveListener(AddPin);
        }

        private void Exit() => 
            OnExit?.Invoke();

        private void AddPin() => 
            OnAddPin?.Invoke();

        private void SkipLevel() => 
            OnSkipLevel?.Invoke();
    }
}