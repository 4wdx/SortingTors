using CodeBase.FSM;
using IState = CodeBase.FSM.IState;

namespace CodeBase.Game.UI.Result.FSM
{
    public abstract class ResultScreenState : IState
    {
        protected readonly StateMachine<ResultScreenState> StateMachine;

        protected ResultScreenState(StateMachine<ResultScreenState> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void Exit();
    }
}