using TMPro;
using UnityEngine;

namespace CodeBase.Game.UI
{
    public class LevelText : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private string _baseText;
        
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _baseText = _text.text;
        }

        public void SetLevel(int level)
        {
            _text.text = _baseText + level.ToString();
        }
    }
}