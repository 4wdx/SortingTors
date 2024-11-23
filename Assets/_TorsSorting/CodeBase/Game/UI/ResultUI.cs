using System;
using CodeBase.Game.UI.Result;
using CodeBase.Root.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.UI
{
    public class ResultUI : MonoBehaviour
    {
        public event Action OnExit;
        
        [SerializeField] private GiftHandler _giftHandler;
        [SerializeField] private CanvasGroup _firstWindow;
        [SerializeField] private Button _exit;
        [SerializeField] private Image _chest;
        [SerializeField] private RectTransform _secondWindow;
        [SerializeField] private Button _exit2;

        private Vector2 _firstWindowPos;
        private Vector2 _secondWindowPos;
        
        public void Initialize(IWalletService walletService)
        {
            //_firstWindowPos = _firstWindow.anchoredPosition;
            //_secondWindowPos = _secondWindow.anchoredPosition;
            _giftHandler.Initialize(walletService);
            _giftHandler.GetComponent<RectTransform>()
                .DOAnchorPos(new Vector2(0, 0), 0.5f);
        }

        public void Show()
        {
            //_exit.onClick.AddListener(Exit);
            //_exit2.onClick.AddListener(Exit);
            
            _firstWindow.DOFade(1f, 0.5f).From(0);
        }

        public void Hide()
        {
            //_exit.onClick.RemoveListener(Exit);
            //_exit2.onClick.RemoveListener(Exit);
        }

        private void Exit() => 
            OnExit?.Invoke();
    }
}