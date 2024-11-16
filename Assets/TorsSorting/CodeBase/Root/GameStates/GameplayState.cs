using CodeBase.Configs;
using CodeBase.FSM;
using CodeBase.Game.Gameplay;
using CodeBase.Root.Services;
using UnityEngine.Networking;

namespace CodeBase.Root
{
    public sealed class GameplayState : GameState, IParametredState<int>
    {
        private readonly PinsHandler _pinsHandler;
        private readonly IGenerationService _generationService;
        private readonly LevelsConfig _levelsConfig;

        public GameplayState(StateMachine<GameState> stateMachine, 
            PinsHandler pinsHandler,
            IGenerationService generationService,
            LevelsConfig levelsConfig) : base(stateMachine)
        {
            _pinsHandler = pinsHandler;
            _generationService = generationService;
            _levelsConfig = levelsConfig;
        }

        public void Enter(int loadingLevel)
        {
            int difficulty = _levelsConfig.GetDifficulty(loadingLevel); 
            PinData[] levelData = _generationService.Generate(7);
            _pinsHandler.Initialize(levelData);
            _pinsHandler.OnComplete += GoTo;
        }
        
        public override void Exit()
        {
            _pinsHandler.OnComplete -= GoTo;
        }

        private void GoTo()
        {
            StateMachine.GoTo<LevelCompleteState>();
        }
    }
}