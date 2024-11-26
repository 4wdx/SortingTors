using CodeBase.Configs;
using CodeBase.FSM;
using CodeBase.Game.Gameplay;
using CodeBase.Game.PlayerInput;
using CodeBase.Game.UI;
using CodeBase.Root.Services;

namespace CodeBase.Root
{
    public sealed class GameplayState : GameState, IParametredState<PinsStack>
    {
        private readonly DragHandler _dragHandler;
        private readonly ISaveService _saveService;
        private readonly GameplayUI _gameplayUI;
        private readonly SkinData _skinData;
        private PinsStack _pinsStack;

        public GameplayState(StateMachine<GameState> stateMachine, 
            GameplayUI gameplayUI,
            DragHandler dragHandler,
            ISaveService saveService) : base(stateMachine)
        {
            _gameplayUI = gameplayUI;
            _dragHandler = dragHandler;
            _saveService = saveService;
        }

        public void Enter(PinsStack pinsStack)
        {
            _gameplayUI.Show();
            _gameplayUI.OnExit += GoToMenu;
            _gameplayUI.OnSkip += SkipLevel;
            
            _pinsStack = pinsStack;
            _pinsStack.StartGameplay();
            _pinsStack.OnComplete += GoToResult;
            
            _dragHandler.Enabled = true;
        }
        
        public override void Exit()
        {
            _gameplayUI.Hide();
            _gameplayUI.OnExit -= GoToMenu;
            _gameplayUI.OnSkip -= SkipLevel;
            
            _pinsStack.OnComplete -= GoToResult;
            _pinsStack = null;
            
            _dragHandler.Enabled = false;
        }

        private void GoToResult() => 
            StateMachine.GoTo<ResultState>();

        private void GoToMenu() => 
            StateMachine.GoTo<MainMenuState>();

        private void SkipLevel()
        {
            _saveService.LevelComplete();
            StateMachine.GoTo<MainMenuState>();
        }
    }
}