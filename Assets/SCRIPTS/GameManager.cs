using EndlessRunner.Common;
using EndlessRunner.Event;
using UnityEngine;

namespace EndlessRunner.Game
{
    public class GameManager : MonoBehaviour, IManager
    {
        private IEventManager eventManager;
        private GameState currentGameState;

        private void Awake() => Application.targetFrameRate = 60;

        public void InitializeManager(IEventManager eventManager)
        {
            SetManagerDependencies(eventManager);
            SetGameState(GameState.MAIN_MENU);
            RegisterEventListeners();
        }

        private void SetManagerDependencies(IEventManager eventManager) => this.eventManager = eventManager;

        private void RegisterEventListeners()
        {
            eventManager.UIEvents.OnStartButtonClicked.AddListener(StartGame);
            eventManager.PlayerEvents.OnHitByObstacle.AddListener(GameOver);
            eventManager.UIEvents.OnRestartButtonClicked.AddListener(RestartGame);
        }

        private void SetGameState(GameState gameState)
        {
            if (currentGameState == gameState) return;

            currentGameState = gameState;
            UpdateTimeScale();

            eventManager.GameEvents.OnGameStateUpdated.Broadcast(currentGameState);
        }

        private void UpdateTimeScale() => Time.timeScale = currentGameState == GameState.IN_GAME ? 1f : 0f;

        private void StartGame() => SetGameState(GameState.IN_GAME);
        private void GameOver() => SetGameState(GameState.GAME_OVER);
        private void RestartGame() => SetGameState(GameState.IN_GAME);
    }
}






