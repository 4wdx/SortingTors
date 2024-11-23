using CodeBase.FSM;
using CodeBase.Game.Gameplay;
using CodeBase.Game.UI;
using CodeBase.Root.Services.LevelFactory;

namespace CodeBase.Root
{
    public class MainMenuState : GameState, IParametredState<int>
    {
        private readonly LevelFactory _levelFactory;
        private readonly MainMenuUI _mainMenuUI;
        private PinsStack _pinsStack;

        public MainMenuState(StateMachine<GameState> stateMachine, 
            MainMenuUI mainMenuUI,
            LevelFactory levelFactory) : base(stateMachine)
        {
            _levelFactory = levelFactory;
            _mainMenuUI = mainMenuUI;
        }

        public void Enter(int loadingLevel = -1)
        {
            _mainMenuUI.gameObject.SetActive(true);
            _mainMenuUI.OnGameplayEnter += GoToGameplay;
            _mainMenuUI.OnShopEnter += GoToShop;
            
            //todo: move out this: _levelText.SetLevel(loadingLevel);


            if (loadingLevel != -1)
            {
                _pinsStack = _levelFactory.CreatePinsStack(loadingLevel);
            }
            else
            {
                _levelFactory.UpdateViewInCurrent();
            }
        }
        
        public override void Exit()
        {
            _mainMenuUI.OnGameplayEnter -= GoToGameplay;
            _mainMenuUI.OnShopEnter -= GoToShop;
            _mainMenuUI.gameObject.SetActive(false);
        }

        private void GoToGameplay() => 
            StateMachine.GoTo<GameplayState, PinsStack>(_pinsStack);

        private void GoToShop() => 
            StateMachine.GoTo<ShopState>();
    }
}