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
            _textMeshPro = GetComponent<TextMeshProUGUI>();
            _baseText = _textMeshPro.text;
            _saveService = saveService;
        }

        private void OnEnable()
        {
            if (_saveService != null)
                _textMeshPro.text = _baseText + _saveService.GetCurrentLevel();
        }
    }
}