using System;
using CodeBase.Game.UI.MainMenu;
using CodeBase.Game.UI.Shop;
using CodeBase.Root.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.UI
{
    public class ShopUI : MonoBehaviour
    {
        public event Action OnExit;
        [SerializeField] private Button _exitButton;
        [SerializeField] private ShopHandler _shopHandler;
        [SerializeField] private AddMoneyButton _addMoneyButton;
        [SerializeField] private UnlockSkinButton[] _unlockSkinButtons;
        [SerializeField] private CurrentMoney _currentMoney;

        public void Initialize(IWalletService walletService, ISkinService skinService, LevelFactory levelFactory)
        {
            _shopHandler.Initialize(skinService, levelFactory);
            _addMoneyButton.Initialize(walletService);
            _currentMoney.Initialize(walletService);
            
            foreach (UnlockSkinButton unlockSkinButton in _unlockSkinButtons) unlockSkinButton.Initialize(walletService, skinService);
            Hide();
        }

        public void Show()
        {
            _shopHandler.gameObject.SetActive(true);
            _exitButton.onClick.AddListener(Exit); 
        }

        public void Hide()
        {           
            _shopHandler.gameObject.SetActive(false);
            _exitButton.onClick.RemoveListener(Exit); 
        }

        private void Exit() => 
            OnExit?.Invoke();
    }
}