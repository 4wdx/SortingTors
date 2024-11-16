using CodeBase.FSM;
using CodeBase.Game.UI;
using CodeBase.Root.Services;
using UnityEngine;

namespace CodeBase.Root
{
    public sealed class LevelCompleteState : GameState, IDefaultState
    {
        private readonly ISaveService _saveService;
        private readonly NextLevelButton _nextLevelButton;
        private readonly ParticleSystem[] _particleSystems;
        private readonly AudioSource _winSound;

        public LevelCompleteState(StateMachine<GameState> stateMachine,
            ISaveService saveService,
            NextLevelButton nextLevelButton,
            ParticleSystem[] particleSystems,
            AudioSource winSound) : base(stateMachine)
        {
            _saveService = saveService;
            _nextLevelButton = nextLevelButton;
            _particleSystems = particleSystems;
            _winSound = winSound;
        }

        public void Enter()
        {
            _saveService.LevelComplete();
            _nextLevelButton.OnClick += GoTo;
            _nextLevelButton.Show();
            
            foreach (ParticleSystem particleSystem in _particleSystems) 
                particleSystem.Play();
            
            _winSound.Play();
        }

        public override void Exit()
        {
            _nextLevelButton.Hide();
            _nextLevelButton.OnClick -= GoTo;
        }

        private void GoTo() => 
            StateMachine.GoTo<GameplayState, int>(_saveService.GetCurrentLevel());
    }
}