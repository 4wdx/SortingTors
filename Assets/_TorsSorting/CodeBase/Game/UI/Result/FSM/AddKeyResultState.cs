using System;
using CodeBase.FSM;
using UnityEngine;

namespace CodeBase.Game.UI.Result.FSM
{
    public class AddKeyResultState : ResultState, IDefaultState
    {
        private readonly AddKeyHandler _addKeyHandler;

        public AddKeyResultState(StateMachine<ResultState> stateMachine, AddKeyHandler addKeyHandler) : base(stateMachine)
        {
            _addKeyHandler = addKeyHandler;
        }

        public void Enter()
        {
            _addKeyHandler.Show();
            _addKeyHandler.OnContinue += GoToExit;
            _addKeyHandler.OnKeyAdd += GoToUseKey;
        }

        public override void Exit()
        {
            _addKeyHandler.OnContinue -= GoToExit;
            _addKeyHandler.OnKeyAdd -= GoToUseKey;
        }

        private void GoToExit() => 
            StateMachine.GoTo<ExitResultState>();

        private void GoToUseKey()
        {
            Debug.Log("Go to use key");
            StateMachine.GoTo<UseKeyResultState, bool>(true);
        }
    }
}