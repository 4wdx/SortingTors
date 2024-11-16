using CodeBase.Root.Services;
using UnityEngine;

namespace CodeBase.Game.UI
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private GameObject _cross;
        private SettingsService _settingsService;
        private bool _enabled;

        public void Initialize(SettingsService settingsService)
        {
            _settingsService = settingsService;
            _enabled = settingsService.GetMusicState();
            _cross.SetActive(!_enabled);
        }

        public void _SwitchState()
        {
            _enabled = !_enabled;
            _settingsService.SetMusicState(_enabled);
            _cross.SetActive(!_enabled);
        }
    }
}