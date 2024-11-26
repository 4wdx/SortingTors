using System;
using CodeBase.Configs;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CodeBase.Game.UI.Shop
{
    public class SkinSelectButton : MonoBehaviour
    {
        public event Action<SkinData> OnSelect;
        
        [SerializeField] private Sprite _selected;
        [SerializeField] private Sprite _unselected;
        [SerializeField] private Sprite _question;
        [SerializeField] private Image _iconImage;
        public SkinData SkinData;
        private bool _canClick;
        
        private Image _borderImage;
        private Button _button;

        private void Awake()
        {
            _borderImage = GetComponent<Image>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(InvokeEvent);
            _canClick = true;
        }
        
        public void SetOpened()
        {
            _borderImage = GetComponent<Image>();
            _canClick = true;
            _borderImage.sprite = _unselected;
            _iconImage.sprite = SkinData.Icon;
        }

        public void SetClosed()
        {
            _borderImage = GetComponent<Image>();
            _canClick = false;
            _borderImage.sprite = _unselected;
            _iconImage.sprite = _question;
        }

        public void SetSelected()
        {
            _canClick = false;
            _borderImage.sprite = _selected;
            _iconImage.sprite = SkinData.Icon;
        }

        private void InvokeEvent()
        {
            if (_canClick == false) 
                return;
            OnSelect?.Invoke(SkinData);
        }
    }
}