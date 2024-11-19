using CodeBase.Configs;
using CodeBase.FSM;
using CodeBase.Game.Gameplay;
using CodeBase.Game.PlayerInput;
using CodeBase.Game.UI;
using CodeBase.Root.Services;
using CodeBase.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Root
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private LevelsConfig _levelsConfig;
        
        [Space, Header("Gameplay components")]
        [SerializeField] private AudioListener _audioListener;
        [SerializeField] private PinsStack _pinsStack;
        [SerializeField] private Mesh _torusMesh;
        
        [Space, Header("UI components")]
        [SerializeField] private NextLevelButton _nextLevelButton;
        [SerializeField] private SoundButton _soundButton;
        [SerializeField] private LevelText _levelText;
        
        [Space, Header("Effects")]
        [SerializeField] private AudioSource _winSound;
        
        [Space, Header("Layers")]
        [SerializeField] private LayerMask _pinsLayerMask;
        [SerializeField] private LayerMask _planeLayerMask;
        
        private StateMachine<GameState> _stateMachine;
        
        private IGenerationService _generationService;
        private SettingsService _settingsService;
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
            _stateMachine.AddState(new GameplayState(_stateMachine, _pinsStack, _generationService, _levelsConfig, _levelText, _dragHandler, _torusMesh));
            _stateMachine.AddState(new LevelCompleteState(_stateMachine, _saveService, _nextLevelButton, _winSound));
        
            _stateMachine.GoTo<GameplayState, int>(_saveService.GetCurrentLevel());
        }

        private void CreateServices()
        {
            DontDestroyOnLoad(gameObject);
            
            _dragHandler = new DragHandler(_pinsLayerMask, _planeLayerMask);
            _saveService = new YgSaveService();
            _generationService = new DefaultGenerationService();
            _settingsService = new SettingsService(_saveService, _audioListener);
            
            _coroutineRunner = new GameObject("[CoroutineRunner]").AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(_coroutineRunner.gameObject);
            
            _updater = new GameObject("[Updater]").AddComponent<Updater>();
            DontDestroyOnLoad(_updater.gameObject);
            
            _updater.Add(_dragHandler);
        }

        private void InitComponents()
        {
            _soundButton.Initialize(_settingsService);
        }
    }
}