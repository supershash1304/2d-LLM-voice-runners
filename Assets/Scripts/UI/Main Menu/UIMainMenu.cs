using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace EndlessRunner.UI
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button quitButton;

        private UIMainMenuController uiMainMenuController;

        private void Awake() => ShowUI();
        private void OnEnable()
        {
            startButton.onClick.AddListener(StartGame);
            quitButton.onClick.AddListener(QuitGame);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(StartGame);
            quitButton.onClick.RemoveListener(QuitGame);
        }

        public void SetController(UIMainMenuController uiMainMenuController) => this.uiMainMenuController = uiMainMenuController;
        public void ShowUI() => this.gameObject.SetActive(true);
        public void HideUI() => this.gameObject.SetActive(false);

        private void StartGame() => uiMainMenuController.OnStartButtonClicked();

        public void QuitGame()
        {
            Debug.Log("Quit Game");
        }
    }
}