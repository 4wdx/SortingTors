using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Root.Services;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Game.UI.Result
{
    public class GiftHandler : MonoBehaviour
    {
        public event Action OnKeysOver;

        [SerializeField] private GameObject[] _keysQueue;
        [SerializeField] private GameObject _keysParent;
        [SerializeField] private GiftButton[] _buttons;
        
        private IWalletService _walletService;
        private IReward[] _rewards;
        private int _keys;
        
        public void Initialize(IWalletService walletService)
        {
            _walletService = walletService;
        }

        public void OnEnable()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Initialize(i);
                _buttons[i].OnTryOpen += TryOpen;
            }
        }

        public void OnDisable()
        {
            foreach (GiftButton button in _buttons)
            {
                button.OnTryOpen -= TryOpen;
            }
        }

        public void Start()
        {
            _keysParent.transform.DOScale(Vector3.one, 0.5f);
            _keys = 3;
            foreach (GameObject key in _keysQueue) 
                key.SetActive(true);
            
            GenerateRewards();
        }

        public void Continue()
        {
            _keysParent.transform.DOScale(Vector3.one, 0.5f);
            _keys = 3;
            foreach (GameObject key in _keysQueue) 
                key.SetActive(true);
        }

        public void Reset()
        {
            for (int i = 0; i < _buttons.Length; i++) 
                _buttons[i].Initialize(i);
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

        private void TryOpen(int rewardIndex)
        {
            if (_keys <= 0) return;
            
            _rewards[rewardIndex].AddReward(_buttons[rewardIndex]);
            RemoveKey();
        }
        
        private void RemoveKey()
        {
            _keys -= 1;
            for (int i = 0; i < _keysQueue.Length; i++) 
                _keysQueue[i].SetActive(i < _keys);

            if (_keys == 0) 
                StartCoroutine(KeysOverRoutine());
        }

        private IEnumerator KeysOverRoutine()
        {
            _keysParent.transform.DOScale(Vector3.zero, 0.5f);
            yield return new WaitForSeconds(0.75f);
            OnKeysOver?.Invoke();
        }
    }
}