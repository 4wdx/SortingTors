using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.UI.Result
{
    public class GiftButton : MonoBehaviour
    {
        public event Action<int> OnTryOpen;

        [SerializeField] private GameObject _moneyIcon;
        [SerializeField] private GameObject _pinIcon;
        [SerializeField] private Image _chest;
        [SerializeField] private TextMeshProUGUI _text;
        private Button _button;
        private bool _activated;
        private int _index;

        public void Initialize(int index)
        {
            _index = index;
            _chest.color = Color.white;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(TryOpen);
        }

        public void RewardPin()
        {
            _text.text = "";
            _pinIcon.SetActive(true);
            _moneyIcon.SetActive(false);
            Animation();
        }

        public void RewardMoney(int value)
        {
            _text.text = "+" + value.ToString();
            _moneyIcon.SetActive(true);
            _pinIcon.SetActive(false);
            Animation();
        }

        private void Animation()
        {
            _chest.DOColor(Color.clear, 0.4f);
        }

        private void TryOpen()
        {
            if (_activated) return;
            
            _activated = true;
            OnTryOpen?.Invoke(_index);
        }
    }
}