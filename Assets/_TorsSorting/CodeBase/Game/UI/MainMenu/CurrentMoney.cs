using CodeBase.Root.Services;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.UI.MainMenu
{
    public class CurrentMoney : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

        public void Initialize(IWalletService walletService)
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            _textMeshProUGUI.text = walletService.CurrentMoney.ToString();
            walletService.OnMoneyChange += UpdateText;
        }

        private void UpdateText(int value)
        {
            _textMeshProUGUI.text = value.ToString();
        }
    }
}