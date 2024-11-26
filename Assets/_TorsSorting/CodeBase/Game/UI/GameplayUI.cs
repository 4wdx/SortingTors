using System;
using CodeBase.Game.UI.Gameplay;
using CodeBase.Game.UI.MainMenu;
using CodeBase.Root.Services;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace CodeBase.Game.UI
{
    public class GameplayUI : MonoBehaviour
    {
        public event Action OnExit;
        public event Action OnSkip;
        
        [SerializeField] private GameObject _parentObject;
        [SerializeField] private Button _exitButton;
        [SerializeField] private AddPinHandler _addPinHandler;
        [SerializeField] private Button _skipLevelButton;
        [SerializeField] private CurrentLevel _currentLevel;

        public void Initialize(IWalletService walletService, LevelFactory levelFactory, ISaveService saveService)
        {
            _addPinHandler.Initialize(walletService, levelFactory);
            Hide();
        }
        
        public void Show()
        {
            _addPinHandler.ResetData();
            _parentObject.SetActive(true);
            _exitButton.onClick.AddListener(Exit);
            _currentLevel.gameObject.SetActive(true);
            _skipLevelButton.onClick.AddListener(StartSkipLevel);
        }

        public void Hide()
        {
            _addPinHandler.ResetData();
            _parentObject.SetActive(false);
            _currentLevel.gameObject.SetActive(false);
            _exitButton.onClick.RemoveListener(Exit);
        }

        private void StartSkipLevel()
        {
            YandexGame.RewVideoShow(90);
            YandexGame.RewardVideoEvent += EndSkipLevel;
        }

        private void EndSkipLevel(int obj)
        {
            if (obj == 90) 
                OnSkip?.Invoke();
            
            YandexGame.RewardVideoEvent -= EndSkipLevel;
        }

        private void Exit() => 
            OnExit?.Invoke();
    }
}