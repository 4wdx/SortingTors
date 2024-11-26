using UnityEditor;

namespace CodeBase.Root.Services
{
    public interface ISaveService
    {
        public void LevelComplete();
        public int GetCurrentLevel();
        
        public void SaveCurrentSkin(int id);
        public bool SkinAvailable(int id);
        public int GetCurrentSkinId();
        public int SkinCountInRarity(int rarity);

        public void SaveCurrentMoney(int value);
        public int GetCurrentMoney();
        
        public void SaveCurrentPin(int value);
        public int GetCurrentPin();
        
        public void SaveSoundState(bool state);
        public bool GetSoundState();
    }
}