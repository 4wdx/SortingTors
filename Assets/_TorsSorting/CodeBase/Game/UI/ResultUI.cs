using System;
using CodeBase.FSM;
using CodeBase.Game.UI.MainMenu;
using CodeBase.Game.UI.Result;
using CodeBase.Game.UI.Result.FSM;
using CodeBase.Root.Services;
using CodeBase.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CodeBase.Game.UI
{
    public class ResultUI : MonoBehaviour
    {
        public event Action OnExit;
        
        [SerializeField] private CanvasGroup _firstWindow;
        [SerializeField] private Image _fillingChest;
        [SerializeField] private RectTransform _effect;
        [Space]
        [SerializeField] private Button _exitButton;
        [Space]
        [SerializeField] private RectTransform _secondWindow;
        [SerializeField] private RectTransform _canvas;
        [SerializeField] private CurrentMoney _currentMoney;
        [Space]
        [SerializeField] private GiftHandler _giftHandler;
        [SerializeField] private AddKeyHandler _addKeyHandler;
        [Space]
        [SerializeField] private Transform[] _toZeroScaleObjects;
        [SerializeField] private Transform[] _toOneScaleObjects;

        private StateMachine<ResultState> _stateMachine;
        private ExitResultState _exitResultState;
        private CoroutineRunner _coroutineRunner;
        private IWalletService _walletService;

        public void Initialize(IWalletService walletService, CoroutineRunner coroutineRunner)
        {
            _walletService = walletService;
            _coroutineRunner = coroutineRunner;
            _currentMoney.Initialize(_walletService);
            
            _giftHandler.Initialize(_walletService);
            Hide();
        }

        public void Show(float progress)
        {
            _firstWindow.interactable = true;
            
            _stateMachine = new StateMachine<ResultState>();
            _exitResultState = new ExitResultState(_stateMachine);
            _exitResultState.OnExit += InvokeExit;
            
            _stateMachine.AddState(new FirstResultState(_stateMachine, _coroutineRunner, _firstWindow, _fillingChest, _effect));
            _stateMachine.AddState(new WaitContinueResultState(_stateMachine, _exitButton));
            _stateMachine.AddState(new OpenRewardedResultState(_stateMachine, _coroutineRunner, _canvas, _secondWindow));
            _stateMachine.AddState(new UseKeyResultState(_stateMachine, _giftHandler));
            _stateMachine.AddState(new AddKeyResultState(_stateMachine, _addKeyHandler));
            _stateMachine.AddState(_exitResultState);
            
            _stateMachine.GoTo<FirstResultState, float>(progress);
        }

        public void Hide()
        {
            foreach (Transform obj in _toOneScaleObjects) 
                obj.transform.localScale = Vector3.one;
            
            foreach (Transform obj in _toZeroScaleObjects) 
                obj.transform.localScale = Vector3.zero;

            _fillingChest.fillAmount = 0f;
            _firstWindow.alpha = 0f;
            _firstWindow.interactable = false;
            _effect.transform.localScale = Vector3.zero;
            
            float height = _canvas.rect.height;
            
            _secondWindow.offsetMax = new Vector2(0, height);
            _secondWindow.offsetMin = new Vector2(0, height);
            Debug.Log(height);
            
            _giftHandler.Reset();
        }

        private void InvokeExit()
        {
            _exitResultState.OnExit -= InvokeExit;
            _exitResultState = null;
            OnExit?.Invoke();
        }
    }
}