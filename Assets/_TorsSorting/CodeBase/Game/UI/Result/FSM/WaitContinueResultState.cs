using CodeBase.FSM;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.UI.Result.FSM
{
    public class WaitContinueResultState : ResultState, IDefaultState
    {
        private readonly Button _exitButton;
        
        public WaitContinueResultState(StateMachine<ResultState> stateMachine,
            Button exitButton) : base(stateMachine)
        {
            _exitButton = exitButton;
        }

        public void Enter()
        {
            _exitButton.transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutBounce);
            _exitButton.onClick.AddListener(GoToMainMenu);
        }

        public override void Exit()
        {
            _exitButton.onClick.RemoveListener(GoToMainMenu);
            _exitButton.transform.localScale = Vector3.zero;
        }

        private void GoToMainMenu()
        {
            StateMachine.GoTo<ExitResultState>();
        }
    }
}