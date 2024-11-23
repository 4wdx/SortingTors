using CodeBase.FSM;
using CodeBase.Game.UI;
using CodeBase.Root.Services;
using CodeBase.Root.Services.LevelFactory;

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
            _shopUI.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            _shopUI.OnExit -= GoInMenu;
            _shopUI.gameObject.SetActive(false);
            _levelFactory.UpdateViewInCurrent();
        }

        private void GoInMenu()
        {
            StateMachine.GoTo<MainMenuState, int>(-1);
        }
    }
}