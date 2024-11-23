using System;
using Unity.VisualScripting;
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

        public void SaveCurrentSkin(int index)
        {
            if (YandexGame.savesData.AvailableSkins[index] == false)
                throw new Exception("Try to set closed skin");

            YandexGame.savesData.CurrentSkin = index;
            YandexGame.SaveProgress();
        }

        public bool CheckAvailableSkin(int index) => 
            YandexGame.savesData.AvailableSkins[index];

        public int GetCurrentSkin() => 
            YandexGame.savesData.CurrentSkin;

        public void SaveCurrentMoney(int value)
        {
            YandexGame.savesData.Money = value;
            YandexGame.SaveProgress();
        }

        public int GetCurrentMoney() => 
            YandexGame.savesData.Money;

        public void SaveCurrentPin(int value)
        {
            YandexGame.savesData.Pin = value;
            YandexGame.SaveProgress();
        }

        public int GetCurrentPin() => 
            YandexGame.savesData.Money;

        public void SaveSoundState(bool state)
        {
            YandexGame.savesData.SoundEnabled = state;
            YandexGame.SaveProgress();
        }

        public bool GetSoundState() => 
            YandexGame.savesData.SoundEnabled;
    }
}