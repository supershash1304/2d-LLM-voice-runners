using EndlessRunner.Common;
using EndlessRunner.Data;
using EndlessRunner.Event;
using EndlessRunner.Game;
using EndlessRunner.Obstacle;
using EndlessRunner.Parallax;
using EndlessRunner.Player;
using EndlessRunner.UI;
using UnityEngine;

namespace EndlessRunner.Main
{
    public class Bootstrap : MonoBehaviour
    {
        private static Bootstrap instance;

        [SerializeField] private GameData gameData;

        private IEventManager eventManager;

        private IManager gameManager;
        private IManager uiManager;
        private IManager playerManager;
        private IManager obstacleManager;
        private IManager parallaxManager;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            CreateEventManager();
            CreateManagers();
            SetManagerDependencies();
        }

        private void CreateEventManager()
        {
            eventManager = new EventManager();
        }

        private void CreateManagers()
        {
            gameManager = GameObject.Instantiate<GameManager>(gameData.GameManagerPrefab, this.transform);
            uiManager = GameObject.Instantiate<UIManager>(gameData.UIManagerPrefab, this.transform);
            playerManager = GameObject.Instantiate<PlayerManager>(gameData.PlayerManagerPrefab, this.transform);
            obstacleManager = GameObject.Instantiate<ObstacleManager>(gameData.ObstacleManagerPrefab, this.transform);
            parallaxManager = GameObject.Instantiate<ParallaxManager>(gameData.ParallaxManagerPrefab, this.transform);
        }

        private void SetManagerDependencies()
        {
            gameManager.InitializeManager(eventManager);
            uiManager.InitializeManager(eventManager);
            playerManager.InitializeManager(eventManager);
            obstacleManager.InitializeManager(eventManager);
            parallaxManager.InitializeManager(eventManager);
        }
    }
}










