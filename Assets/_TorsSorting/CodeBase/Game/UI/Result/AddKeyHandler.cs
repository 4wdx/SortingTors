using System;
using CodeBase.Game.UI.Result.FSM;
using DG.Tweening;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace CodeBase.Game.UI.Result
{
    public class AddKeyHandler : MonoBehaviour 
    { 
        public event Action OnKeyAdd;
        public event Action OnContinue;
        
        [SerializeField] private Button _addKeys;
        [SerializeField] private Button _continueButton;

        public void Show()
        {
            transform.DOScale(Vector3.one, 0.5f);
            _continueButton.onClick.AddListener(ContinueClickHandle);
            _addKeys.onClick.AddListener(AddKeyClickHandle);
        }

        public void Hide()
        {
            transform.localScale = Vector3.zero;
            _continueButton.onClick.RemoveListener(ContinueClickHandle);
            _addKeys.onClick.RemoveListener(AddKeyClickHandle);
        }

        private void AddKeyClickHandle()
        {
            YandexGame.RewVideoShow(99);
            YandexGame.RewardVideoEvent += WaitRewardedShow;
        }

        private void ContinueClickHandle()
        {
            OnContinue?.Invoke();
            Hide();
        }

        private void WaitRewardedShow(int id)
        {
            if (id == 99)
            {
                print("key add event triggered");
                OnKeyAdd?.Invoke();
                Hide();
            }
            
            YandexGame.RewardVideoEvent -= WaitRewardedShow;
        }
    }
}