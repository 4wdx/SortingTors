namespace CodeBase.Root.Services
{
    public struct SettingsValues
    {
        public readonly bool MusicEnabled;
        public readonly bool SfxEnabled;

        public SettingsValues(bool musicEnabled, bool sfxEnabled)
        {
            MusicEnabled = musicEnabled;
            SfxEnabled = sfxEnabled;
        }
    }
}