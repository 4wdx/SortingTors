using YG;

namespace CodeBase.Root.Services
{
    public class YgSaveService : ISaveService
    {
        public void LevelComplete()
        {
            YandexGame.savesData.CurrentLevel++;
            YandexGame.SaveProgress();
        }

        public int GetCurrentLevel()
        {
            return YandexGame.savesData.CurrentLevel;
        }

        public void SaveSettingsValues(SettingsValues settings)
        {
            YandexGame.savesData.MusicEnabled = settings.MusicEnabled;
            YandexGame.savesData.SfxEnabled = settings.SfxEnabled;
            
            YandexGame.SaveProgress();
        }

        public SettingsValues GetSettingsValues()
        {
            return new SettingsValues(
                YandexGame.savesData.MusicEnabled, 
                YandexGame.savesData.SfxEnabled);
        }
    }
}