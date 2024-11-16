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

        public int GetCurrentLevel() => 
            YandexGame.savesData.CurrentLevel;

        public void SaveSoundState(bool state)
        {
            YandexGame.savesData.SoundEnabled = state;
            YandexGame.SaveProgress();
        }

        public bool GetSoundState() => 
            YandexGame.savesData.SoundEnabled;
    }
}