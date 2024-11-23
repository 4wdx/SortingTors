using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.UI
{
    public class ShopUI : MonoBehaviour
    {
        public event Action OnExit;
        [SerializeField] private Button _exitButton;
        
        public void Show()
        {
            _exitButton.onClick.AddListener(Exit); 
        }

        public void Hide()
        {           
            _exitButton.onClick.RemoveListener(Exit); 
        }

        private void Exit() => 
            OnExit?.Invoke();
    }
}