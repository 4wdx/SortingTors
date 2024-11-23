using System;
using CodeBase.Game.UI.MainMenu;
using CodeBase.Root.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public event Action OnGameplayEnter;
        public event Action OnShopEnter;
        
        [SerializeField] private Button _startGameplayButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private SoundButton _soundButton;
        [SerializeField] private CurrentMoney _currentMoney;

        public void Initialize(SettingsService settingsService, IWalletService walletService)
        {
            _soundButton.Initialize(settingsService);
            _currentMoney.Initialize(walletService);
        }
        
        public void Show()
        {
            _startGameplayButton.onClick.AddListener(StartGameplay);
            _shopButton.onClick.AddListener(OpenShop);
        }

        public void Hide()
        {
            _startGameplayButton.onClick.RemoveListener(StartGameplay);
            _shopButton.onClick.RemoveListener(OpenShop);
        }
        
        private void StartGameplay() => 
            OnGameplayEnter?.Invoke();
        
        private void OpenShop() => 
            OnShopEnter?.Invoke();
    }
}