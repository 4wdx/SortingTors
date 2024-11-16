using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.UI
{
    public class NextLevelButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _transitionDuration;
        public event Action OnClick;
        
        private bool _isActive;

        private void Awake()
        {
            _image.color = Color.clear;
            _text.color = Color.clear;
            _image.gameObject.SetActive(false);
        }

        public void Show() => 
            StartCoroutine(ShowAnim());

        public void Hide() => 
            StartCoroutine(HideAnim());

        public void _Click()
        {
            if (_isActive == false) return;
            
            OnClick?.Invoke();
        }

        private IEnumerator ShowAnim()
        {
            _image.gameObject.SetActive(true);
            Color start = Color.clear;
            Color end = Color.white;
            
            float time = 0;
            while (time < _transitionDuration)
            {
                time += Time.deltaTime;
                _image.color = Color.Lerp(start, end, time / _transitionDuration);
                _text.color = Color.Lerp(start, end, time / _transitionDuration);
                yield return null;
            }
            _image.color = end;
            
        }

        private IEnumerator HideAnim()
        {
            Color start = Color.white;
            Color end = Color.clear;
            
            float time = 0;
            while (time < _transitionDuration)
            {
                time += Time.deltaTime;
                _image.color = Color.Lerp(start, end, time / _transitionDuration);
                _text.color = Color.Lerp(start, end, time / _transitionDuration);
                yield return null;
            }
            _image.color = end;
            _image.gameObject.SetActive(false);
        }
    }
}