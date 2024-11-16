using CodeBase.FSM;

namespace CodeBase.Root
{
    public abstract class GameState : IState
    {
        protected StateMachine<GameState> StateMachine;
        
        protected GameState(StateMachine<GameState> stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        public abstract void Exit();
    }
}