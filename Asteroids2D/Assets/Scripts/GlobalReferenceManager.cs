using UnityEngine;

namespace Assets.Scripts
{
    public sealed class GlobalReferenceManager : MonoBehaviour
    {
        public static GlobalReferenceManager ReferenceManager;
        [HideInInspector] public KeyboardInputManager keyboardInputManager;
        [HideInInspector] public PlayerManager playerManager;
        [HideInInspector] public PowerUpManager powerUpManager;
        [HideInInspector] public PowerUpRepository powerUpRepository;
        [HideInInspector] public AsteroidSpawner asteroidSpawner;
        [HideInInspector] public AsteroidSpritesRepository asteroidSpritesRepository;
        [HideInInspector] public EnemyManager enemyManager;
        [HideInInspector] public GameManager gameManager;
        [HideInInspector] public UiManager uiManager;
        [HideInInspector] private AsteroidsPooler asteroidsPooler;

        private void FindReferences()
        {
            keyboardInputManager = FindObjectOfType<KeyboardInputManager>();
            playerManager = FindObjectOfType<PlayerManager>();
            powerUpRepository = FindObjectOfType<PowerUpRepository>();
            powerUpManager = FindObjectOfType<PowerUpManager>();
            asteroidsPooler = FindObjectOfType<AsteroidsPooler>();
            asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
            asteroidSpritesRepository = FindObjectOfType<AsteroidSpritesRepository>();
            enemyManager = FindObjectOfType<EnemyManager>();
            uiManager = FindObjectOfType<UiManager>();
            gameManager = FindObjectOfType<GameManager>();
        }

        private void Initialize()
        {
            gameManager.Initialize();
            playerManager.Intialize();
            asteroidsPooler.Initialize();
            enemyManager.Initialize();
        }

        void Awake()
        {
            ReferenceManager = this;
            FindReferences();
            Initialize();
        }
    }
}
