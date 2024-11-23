using CodeBase.Configs;
using CodeBase.FSM;
using CodeBase.Game.Gameplay;
using CodeBase.Game.PlayerInput;
using CodeBase.Game.UI;

namespace CodeBase.Root
{
    public sealed class GameplayState : GameState, IParametredState<PinsStack>
    {
        private readonly DragHandler _dragHandler;
        private readonly GameplayUI _gameplayUI;
        private readonly SkinData _skinData;
        private PinsStack _pinsStack;

        public GameplayState(StateMachine<GameState> stateMachine, 
            GameplayUI gameplayUI,
            DragHandler dragHandler) : base(stateMachine)
        {
            _gameplayUI = gameplayUI;
            _dragHandler = dragHandler;
        }

        public void Enter(PinsStack pinsStack)
        {
            _gameplayUI.gameObject.SetActive(true);
            _gameplayUI.OnExit += GoToMenu;
            
            _pinsStack = pinsStack;
            _pinsStack.StartGameplay();
            _pinsStack.OnComplete += GoToResult;
            
            _dragHandler.Enabled = true;
        }
        
        public override void Exit()
        {
            _gameplayUI.gameObject.SetActive(false);
            _gameplayUI.OnExit -= GoToMenu;
            
            _pinsStack.OnComplete -= GoToResult;
            _pinsStack = null;
            
            _dragHandler.Enabled = false;
        }

        private void GoToResult() => 
            StateMachine.GoTo<ResultState>();

        private void GoToMenu()
        {
            StateMachine.GoTo<MainMenuState, int>(-1);
        }
    }
}