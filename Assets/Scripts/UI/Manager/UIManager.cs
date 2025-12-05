using EndlessRunner.Common;
using EndlessRunner.Data;
using EndlessRunner.Event;
using UnityEngine;

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
            this.eventManager = eventManager;
            CreateControllers();
            InitializeControllers();
            RegisterEventListeners();
        }

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

            // REGISTER QUIT EVENT HERE
            eventManager.UIEvents.OnQuitButtonClicked.AddListener(QuitGame);
        }

        private void OnGameStateUpdated(GameState state)
        {
            HideAllUIs();
            switch (state)
            {
                case GameState.MAIN_MENU:
                    uiMainMenuController.ShowUI();
                    break;
                case GameState.IN_GAME:
                    uiHUDController.ShowUI();
                    break;
                case GameState.GAME_OVER:
                    uiGameOverMenuController.ShowUI();
                    break;
            }
        }

        private void HideAllUIs()
        {
            uiMainMenuController?.HideUI();
            uiHUDController?.HideUI();
            uiGameOverMenuController?.HideUI();
        }

        // BUTTON HOOKS
        public void OnStartButtonClicked() =>
            eventManager.UIEvents.OnStartButtonClicked.Invoke();

        public void OnRestartGame() =>
            eventManager.UIEvents.OnRestartButtonClicked.Invoke();

        public void OnQuitGame() =>
            eventManager.UIEvents.OnQuitButtonClicked.Invoke(); // FIXED

        private void QuitGame()
        {
            Debug.Log("[UI] QUIT GAME REQUESTED");

            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void OnScoreUpdated(int score) =>
            uiHUDController.OnScoreUpdated(score);

        private void OnGameOver(int finalScore, int highScore) =>
            uiGameOverMenuController.OnGameOver(finalScore, highScore);
    }
}
