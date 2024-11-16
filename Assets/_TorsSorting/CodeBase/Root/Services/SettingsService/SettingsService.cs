using UnityEngine;

namespace CodeBase.Root.Services
{
    public class SettingsService
    {
        private readonly ISaveService _saveService;
        private readonly AudioListener _audioListener;
        private bool _musicEnabled;
        
        public SettingsService(ISaveService saveService, AudioListener audioListener)
        {
            _saveService = saveService;
            _audioListener = audioListener;

            SetMusicState(saveService.GetSoundState());
        }

        public bool GetMusicState() => 
            _musicEnabled;

        public void SetMusicState(bool state)
        {
            _musicEnabled = state;
            _saveService.SaveSoundState(state);
            _audioListener.enabled = _musicEnabled;
        }
    }
}