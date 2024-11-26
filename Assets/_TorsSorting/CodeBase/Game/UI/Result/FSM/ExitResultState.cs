using System;
using CodeBase.FSM;

namespace CodeBase.Game.UI.Result.FSM
{
    public class ExitResultState : ResultState, IDefaultState
    {
        public event Action OnExit;
        
        public ExitResultState(StateMachine<ResultState> stateMachine) : base(stateMachine)
        {
        }

        public void Enter() => 
            OnExit?.Invoke();

        public override void Exit() { }
    }
}