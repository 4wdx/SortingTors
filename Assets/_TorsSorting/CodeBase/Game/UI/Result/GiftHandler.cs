using System.Collections.Generic;
using CodeBase.Root.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Random = UnityEngine.Random;

namespace CodeBase.Game.UI.Result
{
    public class GiftHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _keysParent;
        [SerializeField] private GameObject _addKeysParent;
        [SerializeField] private GameObject _nextParent;
        [Space]
        [SerializeField] private GameObject[] _keysQueue;
        [SerializeField] private GiftButton[] _buttons;
        
        private IWalletService _walletService;
        private IReward[] _rewards;
        private bool _keysAdded;
        
        public void Initialize(IWalletService walletService)
        {
            _walletService = walletService;
            
        }

        public void Show()
        {
            _addKeysParent.transform.localScale = Vector3.zero;
            _nextParent.transform.localScale = Vector3.zero;
            _keysParent.transform.localScale = Vector3.one;
            _keys = 3;
            
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Initialize(i);
                _buttons[i].OnTryOpen += TryOpen;
            }
            
            GenerateRewards();
        }

        private void GenerateRewards()
        {
            List<int> random = new();
            for (int i = 0; i <= 9; i++)
                random.Add(i);
            
            _rewards = new IReward[9];
            for (int i = 0; i < _rewards.Length; i++)
            {
                int x = random[Random.Range(0, random.Count)];
                random.Remove(x);
                if (x == 0)
                    _rewards[i] = new PinReward(_walletService);
                else if (x <= 2)
                    _rewards[i] = new MoneyReward(_walletService, 10);
                else if (x <= 5)
                    _rewards[i] = new MoneyReward(_walletService, 20);
                else
                    _rewards[i] = new MoneyReward(_walletService, 30);
            }
        }

        public void Hide()
        {
            foreach (GiftButton button in _buttons)
                button.OnTryOpen -= TryOpen;
        }

        private void TryOpen(int rewardIndex)
        {
            if (_keys <= 0) return;
            
            _rewards[rewardIndex].AddReward(_buttons[rewardIndex]);
            RemoveKey();
        }
        
        private void RemoveKey()
        {
            _keys -= 1;
            print(_keys);
            for (int i = 0; i < _keysQueue.Length; i++)
            {
                if (i < _keys)
                    _keysQueue[i].SetActive(true);
                else
                    _keysQueue[i].SetActive(false);
            }

            if (_keys == 0 && _keysAdded == false)
            {
                Sequence anim = DOTween.Sequence();
                anim.Append(_keysParent.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce))
                    .Append(_addKeysParent.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce));
            }

            if (_keys == 0 && _keysAdded == true)
            {
                Sequence anim = DOTween.Sequence();
                anim.Append(_keysParent.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBounce))
                    .Append(_nextParent.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce));
            }
        }
        
        private int _keys;

        public void _RewardedShow()
        {
            YandexGame.RewVideoShow(99);
            YandexGame.RewardVideoEvent += AddKeys;
        }

        private void AddKeys(int obj)
        {
            _keys = 3;
            _addKeysParent.transform.localScale = Vector3.zero;
            _keysParent.transform.localScale = Vector3.one;
        }
    }
}