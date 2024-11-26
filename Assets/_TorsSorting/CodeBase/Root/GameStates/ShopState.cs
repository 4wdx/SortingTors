using CodeBase.FSM;
using CodeBase.Game.UI;
using CodeBase.Root.Services;

namespace CodeBase.Root
{
    public class ShopState : GameState, IDefaultState
    {
        private readonly ShopUI _shopUI;
        private readonly LevelFactory _levelFactory;

        public ShopState(StateMachine<GameState> stateMachine,
            ShopUI shopUI,
            LevelFactory levelFactory) : base(stateMachine)
        {
            _shopUI = shopUI;
            _levelFactory = levelFactory;
        }

        public void Enter()
        {
            _shopUI.Show();
            _shopUI.OnExit += GoToMenu;
        }

        public override void Exit()
        {
            _shopUI.OnExit -= GoToMenu;
            _shopUI.Hide();
            _levelFactory.UpdateViewInCurrent();
        }

        private void GoToMenu()
        {
            StateMachine.GoTo<MainMenuState>();
        }
    }
}