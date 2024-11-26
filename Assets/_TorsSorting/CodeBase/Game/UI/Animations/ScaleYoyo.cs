using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.UI.Animations
{
    public class ScaleYoyo : MonoBehaviour
    {
        [SerializeField] private float _targetScale;
        [SerializeField] private float _duration;
        private Tween _tween;
        
        private void OnEnable()
        {
            _tween = transform.DOScale(_targetScale, _duration).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            _tween.Kill();
        }
    }
}