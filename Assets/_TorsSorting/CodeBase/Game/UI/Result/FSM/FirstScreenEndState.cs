using CodeBase.FSM;
using CodeBase.Root;
using UnityEngine.UI;
using YG;

namespace CodeBase.Game.UI.Result.FSM
{
    public class FirstScreenEndState : ResultScreenState, IDefaultState
    {
        private readonly Button _exitButton;
        
        public FirstScreenEndState(StateMachine<ResultScreenState> stateMachine,
            Button exitButton) : base(stateMachine)
        {
            _exitButton = exitButton;
        }

        public void Enter()
        {
            _exitButton.onClick.AddListener(GoToMainMenu);
        }

        public override void Exit()
        {
            _exitButton.onClick.RemoveListener(GoToMainMenu);
        }

        private void GoToMainMenu()
        {
            StateMachine.GoTo<MainMenuState, int>(YandexGame.savesData.CurrentLevel);
        }
    }
}