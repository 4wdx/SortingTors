using CodeBase.FSM;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace CodeBase.Root
{
    public sealed class LevelCompleteState : GameState, IDefaultState
    {
        public LevelCompleteState(StateMachine<GameState> stateMachine) : base(stateMachine)
        {
        }

        public void Enter()
        {
            Debug.Log("Level Complete");
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}