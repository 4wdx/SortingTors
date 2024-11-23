using CodeBase.Configs;
using CodeBase.Game.Gameplay;
using UnityEngine;

namespace CodeBase.Root.Services.LevelFactory 
{
    public class LevelFactory
    {
        private const string LevelBasePath = "Levels/Level ";

        private PinsStack _currentInstance;
        private ISkinService _skinService;

        public LevelFactory(ISkinService skinService)
        {
            _skinService = skinService;
        }

        public PinsStack CreatePinsStack(int levelIndex)
        {
            PinsStack prefab = Resources.Load<PinsStack>(LevelBasePath + levelIndex);
            _currentInstance = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            
            SkinData currentSkin = _skinService.CurrentSkin;
            _currentInstance.SetView(currentSkin);
            
            return _currentInstance;
        }

        public void UpdateViewInCurrent()
        {
            SkinData currentSkin = _skinService.CurrentSkin;
            _currentInstance.SetView(currentSkin);
        }

        public void DestroyCurrent()
        {
            Object.Destroy(_currentInstance.gameObject);
            _currentInstance = null;
        }
    }
}