using CodeBase.FSM;
using IState = CodeBase.FSM.IState;

namespace CodeBase.Game.UI.Result.FSM
{
    public abstract class ResultState : IState
    {
        protected readonly StateMachine<ResultState> StateMachine;

        protected ResultState(StateMachine<ResultState> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void Exit();
    }
}