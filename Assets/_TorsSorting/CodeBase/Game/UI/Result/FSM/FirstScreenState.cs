using CodeBase.FSM;
using DG.Tweening;
using UnityEngine.UI;

namespace CodeBase.Game.UI.Result.FSM
{
    public class FirstScreenState : ResultScreenState, IParametredState<float>
    {
        private readonly Image _progressImage;

        public FirstScreenState(StateMachine<ResultScreenState> stateMachine,
            Image progressImage) : base(stateMachine)
        {
            _progressImage = progressImage;
        }

        public void Enter(float args)
        {
            _progressImage.DOFillAmount(args, 0.5f).From(0);
        }

        public override void Exit()
        {
            
        }
    }
}