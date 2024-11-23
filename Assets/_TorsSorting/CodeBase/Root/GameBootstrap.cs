using CodeBase.Configs;
using CodeBase.FSM;
using CodeBase.Game.Gameplay;
using CodeBase.Game.PlayerInput;
using CodeBase.Game.UI;
using CodeBase.Root.Services;
using CodeBase.Root.Services.LevelFactory;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Root
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [Space, Header("Gameplay components")]
        [SerializeField] private AudioListener _audioListener;
        [SerializeField] private PinsStack _pinsStack;
        [SerializeField] private SkinData _skinData;
        
        [Space, Header("UI components")]
        [SerializeField] private MainMenuUI _mainMenuUI;
        [SerializeField] private GameplayUI _gameplayUI;
        [SerializeField] private ResultUI _resultUI;
        [SerializeField] private ShopUI _shopUI;
        
        [Space, Header("Effects")]
        [SerializeField] private AudioSource _winSound;
        
        [Space, Header("Layers")]
        [SerializeField] private LayerMask _pinsLayerMask;
        [SerializeField] private LayerMask _planeLayerMask;
        
        private StateMachine<GameState> _stateMachine;
        
        private SettingsService _settingsService;
        private IWalletService _walletService;
        private LevelFactory _levelFactory;
        private ISkinService _skinService;
        private ISaveService _saveService;
        private DragHandler _dragHandler;
        
        private CoroutineRunner _coroutineRunner;
        private Updater _updater;
    
        private void Start() => 
            CreateGame();

        private void CreateGame()
        {
            CreateServices();
            InitComponents();
            
            _stateMachine = new StateMachine<GameState>();
            _stateMachine.AddState(new MainMenuState(_stateMachine, _mainMenuUI, _levelFactory));
            _stateMachine.AddState(new GameplayState(_stateMachine, _gameplayUI, _dragHandler));
            _stateMachine.AddState(new ResultState(_stateMachine, _resultUI, _saveService));
            _stateMachine.AddState(new ShopState(_stateMachine, _shopUI, _levelFactory));
        
            //_stateMachine.GoTo<MainMenuState, int>(_saveService.GetCurrentLevel());
            _stateMachine.GoTo<ResultState>();
        }

        private void CreateServices()
        {
            DontDestroyOnLoad(gameObject);
            
            _saveService = new YgSaveService();
            _skinService = new SkinService(_saveService);
            _levelFactory = new LevelFactory(_skinService);
            _walletService = new WalletService(_saveService);
            _settingsService = new SettingsService(_saveService, _audioListener);
            _dragHandler = new DragHandler(_pinsLayerMask, _planeLayerMask)
            {
                Enabled = false
            };

            _coroutineRunner = new GameObject("[CoroutineRunner]").AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(_coroutineRunner.gameObject);
            
            _updater = new GameObject("[Updater]").AddComponent<Updater>();
            DontDestroyOnLoad(_updater.gameObject);
            
            _updater.Add(_dragHandler);
            _skinService.SetSkin(1);
        }

        private void InitComponents()
        {
            _resultUI.Initialize(_walletService);
        }
    }
}