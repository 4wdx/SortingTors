using System.Collections;
using CodeBase.FSM;
using CodeBase.Utils;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.UI.Result.FSM
{
    public class OpenRewardedResultState : ResultState, IDefaultState
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly RectTransform _canvas;
        private readonly RectTransform _secondWindow;

        public OpenRewardedResultState(StateMachine<ResultState> stateMachine,
            CoroutineRunner coroutineRunner,
            RectTransform canvas,
            RectTransform secondWindow) : base(stateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _canvas = canvas;
            _secondWindow = secondWindow;
        }

        public void Enter() => 
            _coroutineRunner.StartCoroutine(Animation());

        public override void Exit() { }

        private IEnumerator Animation()
        {
            _secondWindow.DOAnchorPos(Vector2.zero, 0.5f);
            yield return new WaitForSeconds(0.5f);
            StateMachine.GoTo<UseKeyResultState, bool>(false);
        }
    }
}