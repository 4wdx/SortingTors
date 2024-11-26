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
        
        [SerializeField] private GameObject _parentObject;
        [SerializeField] private Button _startGameplayButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private SoundButton _soundButton;
        [SerializeField] private CurrentMoney _currentMoney;
        [SerializeField] private CurrentLevel _currentLevel;

        public void Initialize(SettingsService settingsService, IWalletService walletService, ISaveService saveService)
        {
            _soundButton.Initialize(settingsService);
            _currentMoney.Initialize(walletService);
            _currentLevel.Initialize(saveService);
            
            Hide();
        }
        
        public void Show()
        {
            _parentObject.SetActive(true);
            _currentLevel.gameObject.SetActive(true);
            _startGameplayButton.onClick.AddListener(StartGameplay);
            _shopButton.onClick.AddListener(OpenShop);
        }

        public void Hide()
        {
            _parentObject.SetActive(false);
            _currentLevel.gameObject.SetActive(false);
            _startGameplayButton.onClick.RemoveListener(StartGameplay);
            _shopButton.onClick.RemoveListener(OpenShop);
        }
        
        private void StartGameplay() => 
            OnGameplayEnter?.Invoke();
        
        private void OpenShop() => 
            OnShopEnter?.Invoke();
    }
}