using System;
using AudioSettings = CodeBase.Configs.AudioSettings;

namespace CodeBase.Root.Services
{
    public class SettingsService
    {
        public Action<SettingsValues> OnSettingsUpdated;
        
        private const float EnabledValue = 0f;
        private const float DisabledValue = -80f;
        
        private readonly ISaveService _saveService;
        private readonly AudioSettings _audioSettings;
        private bool _musicEnabled;
        private bool _sfxEnabled;
        
        public SettingsService(ISaveService saveService, AudioSettings audioSettings)
        {
            _saveService = saveService;
            _audioSettings = audioSettings;

            SetSetting(saveService.GetSettingsValues());
        }

        public void SetSetting(SettingsValues settingsValues)
        {
            _musicEnabled = settingsValues.MusicEnabled;
            _sfxEnabled = settingsValues.SfxEnabled;
            UpdateMixerValues();
            
            _saveService.SaveSettingsValues(settingsValues);
        }

        private void UpdateMixerValues()
        {
            _audioSettings.AudioMixer.SetFloat(_audioSettings.MusicGroup.name, _musicEnabled ? EnabledValue : DisabledValue);
            _audioSettings.AudioMixer.SetFloat(_audioSettings.SfxGroup.name, _sfxEnabled ? EnabledValue : DisabledValue);
        }
    }
}