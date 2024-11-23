using System;
using CodeBase.Root.Services;
using TMPro;
using UnityEngine;
using YG;

namespace CodeBase.Game.UI.MainMenu
{
    public class CurrentLevel : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshPro;
        private ISaveService _saveService;
        private string _baseText;

        public void Initialize(ISaveService saveService)
        {
            _saveService = saveService;
        }
        
        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
            _baseText = _textMeshPro.text;
        }

        private void OnEnable()
        {
            _textMeshPro.text = _baseText + _saveService.GetCurrentLevel();
        }
    }
}