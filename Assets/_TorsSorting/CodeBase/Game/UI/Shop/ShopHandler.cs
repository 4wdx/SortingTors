using System;
using CodeBase.Configs;
using CodeBase.Root.Services;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

namespace CodeBase.Game.UI.Shop
{
    public class ShopHandler : MonoBehaviour
    {
        [SerializeField] private GameObject[] _pages;
        [SerializeField] private SkinSelectButton[] _skinSelectButtons;
        [SerializeField] private ShopViewHandler _shopViewHandler;
        private ISkinService _skinService;
        private LevelFactory _levelFactory; 

        public void Initialize(ISkinService skinService, LevelFactory levelFactory)
        {
            _skinService = skinService;
            _levelFactory = levelFactory;
            
            foreach (SkinSelectButton button in _skinSelectButtons)
            {
                button.OnSelect += SetNewSkin;
            }
        }

        private void OnEnable() => 
            UpdateButtons();

        public void UnlockSkin(int pageNumber)
        {
            if (pageNumber == 0)
                UnlockRandom(0, 8);
            else if (pageNumber == 1)
                UnlockRandom(8, 16);
            else 
                UnlockRandom(16, 24);
        }

        private void UnlockRandom(int min, int max)
        {
            int rand = Random.Range(min, max);
            while (YandexGame.savesData.AvailableSkins[rand])
            {
                rand = Random.Range(min, max);
                
            }
            print(rand);
            SetNewSkin(_skinSelectButtons[rand].SkinData);
        }

        private void UpdateButtons()
        {
            for (int i = 0; i < _skinSelectButtons.Length; i++)
            {
                if (YandexGame.savesData.AvailableSkins[i])
                    _skinSelectButtons[i].SetOpened();
                else 
                    _skinSelectButtons[i].SetClosed();
            }
            
            _skinSelectButtons[YandexGame.savesData.CurrentSkin].SetSelected();
        }

        private void SetNewSkin(SkinData newSkin)
        {
            _skinService.SetSkin(newSkin);
            //_levelFactory.UpdateViewInCurrent();
            UpdateButtons();
        }
    }
}