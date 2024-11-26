using CodeBase.FSM;
using CodeBase.Game.Gameplay;
using CodeBase.Game.UI;
using CodeBase.Root.Services;
using YG;

namespace CodeBase.Root
{
    public class MainMenuState : GameState, IParametredState<int>, IDefaultState
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

        public void Enter(int loadingLevel)
        {
            _levelFactory.DestroyCurrent();
            _mainMenuUI.Show();
            _mainMenuUI.OnGameplayEnter += GoToGameplay;
            _mainMenuUI.OnShopEnter += GoToShop;
            
            _pinsStack = _levelFactory.CreatePinsStack(loadingLevel);
        }

        public void Enter()
        {
            _levelFactory.DestroyCurrent();
            _mainMenuUI.Show();
            _mainMenuUI.OnGameplayEnter += GoToGameplay;
            _mainMenuUI.OnShopEnter += GoToShop;
            
            _pinsStack = _levelFactory.CreatePinsStack(YandexGame.savesData.CurrentLevel);
        }

        public override void Exit()
        {
            _mainMenuUI.OnGameplayEnter -= GoToGameplay;
            _mainMenuUI.OnShopEnter -= GoToShop;
            _mainMenuUI.Hide();
        }

        private void GoToGameplay() => 
            StateMachine.GoTo<GameplayState, PinsStack>(_pinsStack);

        private void GoToShop() => 
            StateMachine.GoTo<ShopState>();
    }
}