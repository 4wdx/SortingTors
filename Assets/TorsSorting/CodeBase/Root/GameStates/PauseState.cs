using CodeBase.FSM;

namespace CodeBase.Root
{
    public sealed class PauseState : GameState, IDefaultState
    {
        public PauseState(StateMachine<GameState> stateMachine) : base(stateMachine)
        {
        }

        public void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}