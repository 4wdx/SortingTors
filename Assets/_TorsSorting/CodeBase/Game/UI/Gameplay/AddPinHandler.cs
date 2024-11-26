using System;
using CodeBase.Root.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace CodeBase.Game.UI.Gameplay
{
    public class AddPinHandler : MonoBehaviour
    {
        [SerializeField] private Button _addPin;
        [SerializeField] private TextMeshProUGUI _pinCountText;
        [SerializeField] private Button _getPin;
        private IWalletService  _walletService;
        private LevelFactory _levelFactory;
        private int _addCount;
        
        public void Initialize(IWalletService walletService, LevelFactory levelFactory)
        {
            _walletService = walletService;
            _levelFactory = levelFactory;
            
            _addPin.onClick.AddListener(AddPinOnClick);
            _getPin.onClick.AddListener(StartGetPin);
            walletService.OnPinChange += UpdateStatus;
            _addCount = 0;
        }

        public void ResetData()
        {
            _addCount = 0;
        }

        private void UpdateStatus(int value)
        {
            _addPin.interactable = value > 0;
            
            if (_addCount >= 2) _addPin.interactable = false;
            
            _pinCountText.text = value.ToString();
        }

        private void OnEnable()
        {
            UpdateStatus(_walletService.CurrentPin);
        }

        private void AddPinOnClick()
        {
            _addCount++;
            _levelFactory.AddPin();
            _walletService.RemovePin();
        }

        private void StartGetPin()
        {
            YandexGame.RewVideoShow(95);
            YandexGame.RewardVideoEvent += EndGetPin;
        }

        private void EndGetPin(int obj)
        {
            if (obj == 95)
            {
                _walletService.AddPin();
            }
            YandexGame.RewardVideoEvent -= EndGetPin;
        }
    }
}