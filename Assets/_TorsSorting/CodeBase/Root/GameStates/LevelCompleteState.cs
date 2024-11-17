using CodeBase.FSM;
using CodeBase.Game.UI;
using CodeBase.Root.Services;
using UnityEngine;
using YG;

namespace CodeBase.Root
{
    public sealed class LevelCompleteState : GameState, IDefaultState
    {
        private readonly ISaveService _saveService;
        private readonly NextLevelButton _nextLevelButton;
        private readonly AudioSource _winSound;

        public LevelCompleteState(StateMachine<GameState> stateMachine,
            ISaveService saveService,
            NextLevelButton nextLevelButton,
            AudioSource winSound) : base(stateMachine)
        {
            _saveService = saveService;
            _nextLevelButton = nextLevelButton;
            _winSound = winSound;
        }

        public void Enter()
        {
            _saveService.LevelComplete();
            _nextLevelButton.OnClick += GoTo;
            _nextLevelButton.Show();
            
            _winSound.Play();
        }

        public override void Exit()
        {
            _nextLevelButton.Hide();
            _nextLevelButton.OnClick -= GoTo;
        }

        private void GoTo()
        {
            YandexGame.FullscreenShow();
            StateMachine.GoTo<GameplayState, int>(_saveService.GetCurrentLevel());
        }
    }
}