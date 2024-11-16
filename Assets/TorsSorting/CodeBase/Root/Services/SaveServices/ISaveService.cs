using UnityEditor;

namespace CodeBase.Root.Services
{
    public interface ISaveService
    {
        public void LevelComplete();
        
        public int GetCurrentLevel();

        public void SaveSettingsValues(SettingsValues settings);
        
        public SettingsValues GetSettingsValues();
    }
}