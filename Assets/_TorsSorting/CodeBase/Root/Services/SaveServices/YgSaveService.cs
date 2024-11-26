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

        public void SaveCurrentSkin(int id)
        {
            if (YandexGame.savesData.AvailableSkins[id] == false)
            {
                if (id < 8)
                    YandexGame.savesData.CommonSkins++;
                else if (id < 16)
                    YandexGame.savesData.RareSkins++;
                else
                    YandexGame.savesData.LegendSkins++;
                
                YandexGame.savesData.AvailableSkins[id] = true;
            }

            YandexGame.savesData.CurrentSkin = id;
            YandexGame.SaveProgress();
        }

        public bool SkinAvailable(int id) => 
            YandexGame.savesData.AvailableSkins[id];

        public int GetCurrentSkinId() => 
            YandexGame.savesData.CurrentSkin;

        public int SkinCountInRarity(int rarity)
        {
            return rarity switch
            {
                0 => YandexGame.savesData.CommonSkins,
                1 => YandexGame.savesData.RareSkins,
                2 => YandexGame.savesData.LegendSkins
            };
        }

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
            YandexGame.savesData.Pin;

        public void SaveSoundState(bool state)
        {
            YandexGame.savesData.SoundEnabled = state;
            YandexGame.SaveProgress();
        }

        public bool GetSoundState() => 
            YandexGame.savesData.SoundEnabled;
    }
}