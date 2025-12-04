using EndlessRunner.Common;
using EndlessRunner.Data;
using EndlessRunner.Event;
using UnityEngine;

namespace EndlessRunner.Obstacle
{
    public class ObstacleManager : MonoBehaviour, IManager
    {
        [SerializeField] private ObstacleData obstacleData;

        private IEventManager eventManager;
        private GameState currentGameState;

        private ObstaclePool obstaclePool;
        private ObstacleSpawner obstacleSpawner;

        public void InitializeManager(IEventManager eventManager)
        {
            SetManagerDependencies(eventManager);
            RegisterEventListeners();
        }

        private void SetManagerDependencies(IEventManager eventManager) => this.eventManager = eventManager;

        private void CreateObstaclePool() 
        {
            obstaclePool = new ObstaclePool(obstacleData, this);
            obstaclePool.InitializePool();
        }

        private void CreateObstacleSpawner()
        {
            obstacleSpawner = new ObstacleSpawner(this);
        }

        private void RegisterEventListeners()
        {
            eventManager.GameEvents.OnGameStateUpdated.AddListener(OnGameStateUpdated);
        }

        private void OnGameStateUpdated(GameState currentGameState)
        {
            this.currentGameState = currentGameState;

            switch (currentGameState)
            {
                case GameState.MAIN_MENU:
                    break;
                case GameState.IN_GAME:
                    if (obstaclePool == null) CreateObstaclePool();
                    if (obstacleSpawner == null) CreateObstacleSpawner();
                    obstacleSpawner.StartSpawning();
                    break;
                case GameState.GAME_OVER:
                    obstacleSpawner?.StopSpawning();
                    HideAllActiveObstacles();
                    break;
            }
        }

        public ObstaclePool GetPool() => obstaclePool;
        public ObstacleData GetData() => obstacleData;
        public void OnObstacleAvoided(int scoreValue) => eventManager.ObstacleEvents.OnObstacleAvoided.Invoke(scoreValue);
        private void HideAllActiveObstacles() => obstaclePool?.ReleaseAllActiveObstacles();
    }
}