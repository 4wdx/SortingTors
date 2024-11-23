using System;
using CodeBase.Root.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.UI.MainMenu
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _disabledSprite;
        private Button _button;
        private Image _image;
        private SettingsService _settingsService;

        public void Initialize(SettingsService settingsService)
        {
            _settingsService = settingsService;
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _button.onClick.AddListener(SwitchSound);
        }

        private void SwitchSound()
        {
            _settingsService.SetMusicState(!_settingsService.GetMusicState());
            _image.sprite = _settingsService.GetMusicState() ? _enabledSprite : _disabledSprite;
        }
    }
}