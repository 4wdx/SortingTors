using CodeBase.Configs;
using CodeBase.FSM;
using CodeBase.Game.Gameplay;
using CodeBase.Root.Services;
using CodeBase.Utils;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using AudioSettings = CodeBase.Configs.AudioSettings;

namespace CodeBase.Root
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private AudioSettings _audioSettings;
        [SerializeField] private LevelsConfig _levelsConfig;
        
        [Space, Header("SceneComponents")]
        [SerializeField] private PinsHandler _pinsHandler;
        [SerializeField] private LayerMask _pinsLayerMask;
        [SerializeField] private LayerMask _planeLayerMask;
        
        private StateMachine<GameState> _stateMachine;
        
        private DragHandler _dragHandler;
        private ISaveService _saveService;
        private SettingsService _settingsService;
        private IGenerationService _generationService;
        
        private CoroutineRunner _coroutineRunner;
        private Updater _updater;
    
        private void Awake() => 
            CreateGame();

        private void CreateGame()
        {
            CreateServices();
            InitComponents();
            
            _stateMachine = new StateMachine<GameState>();
            _stateMachine.AddState(new GameplayState(_stateMachine, _pinsHandler, _generationService, _levelsConfig));
            _stateMachine.AddState(new LevelCompleteState(_stateMachine));
            _stateMachine.AddState(new PauseState(_stateMachine));
        
            _stateMachine.GoTo<GameplayState, int>(20);
        }

        private void CreateServices()
        {
            DontDestroyOnLoad(gameObject);
            
            _dragHandler = new DragHandler(_pinsLayerMask, _planeLayerMask);
            _saveService = new YgSaveService();
            _generationService = new DefaultGenerationService();
            _settingsService = new SettingsService(_saveService, _audioSettings);
            
            _coroutineRunner = new GameObject("[CoroutineRunner]").AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(_coroutineRunner.gameObject);
            
            _updater = new GameObject("[Updater]").AddComponent<Updater>();
            DontDestroyOnLoad(_updater.gameObject);
            
            _updater.Add(_dragHandler);
        }

        private void InitComponents()
        {
            
        }
    }
}