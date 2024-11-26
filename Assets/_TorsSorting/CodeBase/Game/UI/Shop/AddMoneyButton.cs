using CodeBase.Root.Services;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace CodeBase.Game.UI.Shop
{
    public class AddMoneyButton : MonoBehaviour
    {
        [SerializeField] private int _money;
        private IWalletService _walletService;
        private Button _button;

        public void Initialize(IWalletService walletService)
        {
            _walletService = walletService;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(AddMoneyStart);
        }

        private void AddMoneyStart()
        {
            YandexGame.RewVideoShow(98);
            YandexGame.RewardVideoEvent += AddMoneyComplete;
        }

        private void AddMoneyComplete(int obj)
        {
            if (obj == 98) 
                _walletService.AddMoney(_money);
            
            YandexGame.RewardVideoEvent -= AddMoneyComplete;
        }
    }
}