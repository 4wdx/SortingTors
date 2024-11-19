using System.Collections;
using CodeBase.Configs;
using CodeBase.FSM;
using CodeBase.Game.Gameplay;
using CodeBase.Game.PlayerInput;
using CodeBase.Game.UI;
using CodeBase.Root.Services;
using UnityEngine;

namespace CodeBase.Root
{
    public sealed class GameplayState : GameState, IParametredState<int>
    {
        private readonly PinsStack _pinsStack;
        private readonly IGenerationService _generationService;
        private readonly LevelsConfig _levelsConfig;
        private readonly LevelText _levelText;
        private readonly DragHandler _dragHandler;
        private readonly Mesh _mesh;

        public GameplayState(StateMachine<GameState> stateMachine, 
            PinsStack pinsStack,
            IGenerationService generationService,
            LevelsConfig levelsConfig,
            LevelText levelText,
            DragHandler dragHandler,
            Mesh mesh) : base(stateMachine)
        {
            _pinsStack = pinsStack;
            _generationService = generationService;
            _levelsConfig = levelsConfig;
            _levelText = levelText;
            _dragHandler = dragHandler;
            _mesh = mesh;
        }

        public void Enter(int loadingLevel)
        {
            _levelText.SetLevel(loadingLevel);
            _dragHandler.Enabled = true;
            
            /*int difficulty = _levelsConfig.GetDifficulty(loadingLevel); 
            Debug.Log(difficulty);
            PinData[] levelData = _generationService.Generate(difficulty);
            
            _pinsHandler.Initialize(levelData);*/

            //_pinsStack.StartCoroutine(LoadLevel(loadingLevel));
            _pinsStack.Initialize(_mesh);
            _pinsStack.OnComplete += GoTo;
        }
        
        public override void Exit()
        {
            _pinsStack.OnComplete -= GoTo;
            _dragHandler.Enabled = false;
        }

        private void GoTo() => 
            StateMachine.GoTo<LevelCompleteState>();

        private IEnumerator LoadLevel(int loadingLevel)
        {
            int difficulty = _levelsConfig.GetDifficulty(loadingLevel); 
            Debug.Log(difficulty);
            PinData[] levelData = _generationService.Generate(difficulty);
            
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            
            //_pinsStack.Initialize(levelData);
            
        }
    }
}