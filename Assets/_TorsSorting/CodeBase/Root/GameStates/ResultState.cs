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
            int currentCompletedLevel = _saveService.GetCurrentLevel();
            float progressToBox;

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (currentCompletedLevel % 5 == 0)
                progressToBox = 1f;
            else
                progressToBox = (float)(currentCompletedLevel % 5) / 5;
            
            _resultUI.Show(progressToBox);
            _saveService.LevelComplete();
            _resultUI.OnExit += GoTo;
        }

        public override void Exit()
        {
            _resultUI.Hide();
            _resultUI.OnExit -= GoTo;
        }

        private void GoTo()
        {
            YandexGame.FullscreenShow();
            StateMachine.GoTo<MainMenuState, int>(_saveService.GetCurrentLevel());
        }
    }
}