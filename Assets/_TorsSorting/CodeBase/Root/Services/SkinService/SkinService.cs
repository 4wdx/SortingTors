using CodeBase.Configs;
using UnityEngine;

namespace CodeBase.Root.Services
{
    public class SkinService : ISkinService
    {
        public SkinData CurrentSkin {get; private set; }

        private const string BaseSkinPath = "Skins/SkinData ";
        private readonly ISaveService _saveService;
        private SkinData _currentSkin;
        
        public SkinService(ISaveService saveService)
        {
            _saveService = saveService;
            SkinData currentSkin =  Resources.Load<SkinData>(BaseSkinPath + saveService.GetCurrentSkinId());
            SetSkin(currentSkin);
        }
        
        public void SetSkin(SkinData skinData)
        {
            _saveService.SaveCurrentSkin(skinData.Id);
            //SkinData skinData = Resources.Load<SkinData>(BaseSkinPath + id.ToString());
            
            CurrentSkin = skinData;
        }

        public int GetCountInRarity(int rarity) => 
            _saveService.SkinCountInRarity(rarity);
    }
}