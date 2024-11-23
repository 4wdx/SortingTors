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
            SetSkin(saveService.GetCurrentSkin());
        }
        
        public void SetSkin(int id)
        {
            _saveService.SaveCurrentSkin(id);
            SkinData skinData = Resources.Load<SkinData>(BaseSkinPath + id.ToString());
            
            CurrentSkin = skinData;
        }
    }
}