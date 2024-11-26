using System.Collections;
using System.Collections.Generic;
using CodeBase.FSM;
using CodeBase.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.UI.Result.FSM
{
    public class FirstResultState : ResultState, IParametredState<float>
    {
        private readonly CoroutineRunner _coroutineRunner;
        private readonly CanvasGroup _canvasGroup;
        private readonly Image _progressImage;
        private readonly RectTransform _raysEffect;

        public FirstResultState(StateMachine<ResultState> stateMachine,
            CoroutineRunner coroutineRunner,
            CanvasGroup canvasGroup,
            Image progressImage,
            RectTransform raysEffect) : base(stateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _canvasGroup = canvasGroup;
            _progressImage = progressImage;
            _raysEffect = raysEffect;
        }

        public void Enter(float progress)
        {
            _coroutineRunner.StartCoroutine(Animation(progress));
        }

        public override void Exit()
        {
            
        }

        private IEnumerator Animation(float progress)
        {
            yield return new WaitForSeconds(1.5f); //wait some time
            
            yield return _canvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutQuint).WaitForCompletion();
            yield return new WaitForSeconds(0.25f);
            yield return _progressImage.DOFillAmount(progress, progress * 2).SetEase(Ease.InCubic).WaitForCompletion();
            

            if (Mathf.Approximately(progress, 1f)) 
            {
                _raysEffect.DORotate(new Vector3(0, 0, 360), 1.4f, RotateMode.FastBeyond360).SetLoops(3).SetEase(Ease.Linear);
                yield return _raysEffect.DOScale(Vector3.one, 0.5f).SetEase(Ease.Linear).WaitForCompletion();
                yield return new WaitForSeconds(1f);
                StateMachine.GoTo<OpenRewardedResultState>();
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                StateMachine.GoTo<WaitContinueResultState>();
            }
        }
    }
}