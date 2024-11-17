using System.Collections;
using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.FSM;
using CodeBase.Game.Gameplay;
using CodeBase.Game.UI;
using CodeBase.Root.Services;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Root
{
    public sealed class GameplayState : GameState, IParametredState<int>
    {
        private readonly PinsHandler _pinsHandler;
        private readonly IGenerationService _generationService;
        private readonly LevelsConfig _levelsConfig;
        private readonly LevelText _levelText;
        private readonly DragHandler _dragHandler;

        public GameplayState(StateMachine<GameState> stateMachine, 
            PinsHandler pinsHandler,
            IGenerationService generationService,
            LevelsConfig levelsConfig,
            LevelText levelText,
            DragHandler dragHandler) : base(stateMachine)
        {
            _pinsHandler = pinsHandler;
            _generationService = generationService;
            _levelsConfig = levelsConfig;
            _levelText = levelText;
            _dragHandler = dragHandler;
        }

        public void Enter(int loadingLevel)
        {
            _levelText.SetLevel(loadingLevel);
            _dragHandler.Enabled = true;
            
            /*int difficulty = _levelsConfig.GetDifficulty(loadingLevel); 
            Debug.Log(difficulty);
            PinData[] levelData = _generationService.Generate(difficulty);
            
            _pinsHandler.Initialize(levelData);*/

            _pinsHandler.StartCoroutine(LoadLevel(loadingLevel));
            _pinsHandler.OnComplete += GoTo;
        }
        
        public override void Exit()
        {
            _pinsHandler.OnComplete -= GoTo;
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
            
            _pinsHandler.Initialize(levelData);
            
        }
    }
}