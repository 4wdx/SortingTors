using CodeBase.FSM;
using CodeBase.Game.UI;
using CodeBase.Root.Services;
using UnityEngine;
using YG;

namespace CodeBase.Root
{
    public sealed class ResultState : GameState, IDefaultState
    {
        private readonly ISaveService _saveService;
        private readonly ResultUI _resultUI;

        public ResultState(StateMachine<GameState> stateMachine,
            ResultUI resultUI,
            ISaveService saveService) : base(stateMachine)
        {
            _resultUI = resultUI;
            _saveService = saveService;
        }

        public void Enter()
        {
            _saveService.LevelComplete();
            _resultUI.gameObject.SetActive(true);
            _resultUI.OnExit += GoTo;
        }

        public override void Exit()
        {
            _resultUI.OnExit -= GoTo;
            _resultUI.gameObject.SetActive(false);
        }

        private void GoTo()
        {
            YandexGame.FullscreenShow();
            StateMachine.GoTo<MainMenuState, int>(_saveService.GetCurrentLevel());
        }
    }
}