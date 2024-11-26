using CodeBase.Configs;
using CodeBase.Game.Gameplay;
using UnityEngine;

namespace CodeBase.Root.Services 
{
    public class LevelFactory
    {
        private readonly ISkinService _skinService;
        private const string LevelBasePath = "Levels/Level ";
        private const string SkinBasePath = "Skins/SkinData ";

        private PinsStack _currentInstance;

        public LevelFactory(ISkinService skinService)
        {
            _skinService = skinService;
        }

        public PinsStack CreatePinsStack(int levelIndex)
        {
            if (levelIndex > 10)
                levelIndex = (levelIndex % 5) + 5;
            
            PinsStack prefab = Resources.Load<PinsStack>(LevelBasePath + levelIndex);
            _currentInstance = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            
            _currentInstance.SetView(_skinService.CurrentSkin);
            
            return _currentInstance;
        }

        public void UpdateViewInCurrent()
        {
            _currentInstance.SetView(_skinService.CurrentSkin);
        }

        public void AddPin()
        {
            _currentInstance.AddPin();
        }

        public void DestroyCurrent()
        {
            if (_currentInstance == null) 
                return;
            Object.Destroy(_currentInstance.gameObject);
            _currentInstance = null;
        }
    }
}