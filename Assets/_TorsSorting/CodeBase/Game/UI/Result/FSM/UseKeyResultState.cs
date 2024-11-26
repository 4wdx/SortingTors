using CodeBase.FSM;
using UnityEngine;

namespace CodeBase.Game.UI.Result.FSM
{
    public class UseKeyResultState : ResultState, IParametredState<bool>
    {
        private readonly GiftHandler _giftHandler;

        public UseKeyResultState(StateMachine<ResultState> stateMachine,
            GiftHandler giftHandler) : base(stateMachine)
        {
            _giftHandler = giftHandler;
        }

        public void Enter(bool keysAddedPrevious)
        {
            //todo: key, after key if true - to continue, elif false - to addKeyState

            if (keysAddedPrevious == false)
            {
                _giftHandler.Start();
                _giftHandler.OnKeysOver += GoToAddKey;
            }
            else
            {
                Debug.Log("continue");
                _giftHandler.Continue();
                _giftHandler.OnKeysOver += GoToWaitContinue;
            }
        }

        public override void Exit()
        {
            _giftHandler.OnKeysOver -= GoToWaitContinue;
            _giftHandler.OnKeysOver -= GoToAddKey;
        }

        private void GoToAddKey() => 
            StateMachine.GoTo<AddKeyResultState>();

        private void GoToWaitContinue() => 
            StateMachine.GoTo<WaitContinueResultState>();
    }
}