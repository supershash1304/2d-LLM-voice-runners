using EndlessRunner.Common;
using EndlessRunner.Data;
using EndlessRunner.Event;
using UnityEngine;
using System.Collections.Generic;

namespace EndlessRunner.UI
{
    public class UIManager : MonoBehaviour, IManager
    {
        [SerializeField] private UIData uiData;
        [SerializeField] private Canvas uiCanvas;

        private IEventManager eventManager;

        private UIMainMenuController uiMainMenuController;
        private UIHUDController uiHUDController;
        private UIGameOverMenuController uiGameOverMenuController;

        public void InitializeManager(IEventManager eventManager)
        {
            SetManagerDependencies(eventManager);
            CreateControllers();
            InitializeControllers();
            RegisterEventListeners();
        }

        private void SetManagerDependencies(IEventManager eventManager) => this.eventManager = eventManager;              

        private void CreateControllers()
        {
            uiMainMenuController = new UIMainMenuController(uiData, uiCanvas, this);
            uiHUDController = new UIHUDController(uiData, uiCanvas, this);
            uiGameOverMenuController = new UIGameOverMenuController(uiData, uiCanvas, this);
        }

        private void InitializeControllers()
        {
            uiMainMenuController.InitializeController();
            uiHUDController.InitializeController();
            uiGameOverMenuController.InitializeController();
        }

        private void RegisterEventListeners()
        {
            eventManager.GameEvents.OnGameStateUpdated.AddListener(OnGameStateUpdated);
            eventManager.PlayerEvents.OnScoreUpdated.AddListener(OnScoreUpdated);
            eventManager.PlayerEvents.OnGameover.AddListener(OnGameOver);
        }

        private void OnGameStateUpdated(GameState currentGameState)
        {
            HideAllUIs();

            switch (currentGameState)
            {
                case GameState.MAIN_MENU:
                    uiMainMenuController.ShowUI();
                    break;
                case GameState.IN_GAME:
                    uiHUDController.ShowUI();
                    break;
                case GameState.GAME_OVER:
                    uiGameOverMenuController?.ShowUI();
                    break;
            }
        }

        private void HideAllUIs()
        {
            uiMainMenuController?.HideUI();
            uiHUDController?.HideUI();
            uiGameOverMenuController?.HideUI();
        }

        public void OnStartButtonClicked() => eventManager.UIEvents.OnStartButtonClicked.Invoke();

        public void OnScoreUpdated(int playerScore) => uiHUDController.OnScoreUpdated(playerScore);

        private void OnGameOver(int finalScore, int highScore)
        {
            uiGameOverMenuController.OnGameOver(finalScore, highScore);
        }



        public void OnRestartGame() => eventManager.UIEvents.OnStartButtonClicked.Invoke();
        public void OnQuitGame() => Debug.Log("Quit");
    }
}















