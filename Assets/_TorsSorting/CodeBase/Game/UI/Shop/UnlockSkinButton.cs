using System;
using CodeBase.Root.Services;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

namespace CodeBase.Game.UI.Shop
{
    public class UnlockSkinButton : MonoBehaviour
    {
        [SerializeField] private ShopHandler _shopHandler;
        [SerializeField] private int _cost;
        [SerializeField] private int _page;
        private IWalletService _walletService;
        private ISkinService _skinService;
        private Button _button;

        public void Initialize(IWalletService walletService, ISkinService skinService)
        {
            _button = GetComponent<Button>();
            _walletService = walletService;
            _skinService = skinService;
            _walletService.OnMoneyChange += CheckAvailable;
            CheckAvailable();
        }

        private void CheckAvailable(int obj)
        {
            CheckAvailable();
        }

        private void OnEnable()
        {
            if (_button == null) 
                return;
            
            _button.onClick.AddListener(Unlock);
            CheckAvailable();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Unlock);
        }

        private void CheckAvailable()
        {
            print(_walletService.CurrentMoney);
            if (_walletService.CurrentMoney >= _cost && CheckFullSkins() == false)
                _button.interactable = true;
            else 
                _button.interactable = false;
        }

        private void Unlock()
        {
            _shopHandler.UnlockSkin(_page);
            _walletService.TryRemoveMoney(_cost);
            CheckAvailable();
        }

        private bool CheckFullSkins() => 
            _skinService.GetCountInRarity(_page) >= 8;
    }
}