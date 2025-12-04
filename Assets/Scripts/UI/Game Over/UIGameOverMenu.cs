using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace EndlessRunner.UI
{
    public class UIGameOverMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highscoreText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;

        private UIGameOverMenuController controller;

        private void Awake() => HideUI();

        private void OnEnable()
        {
            restartButton.onClick.AddListener(RestartGame);
            quitButton.onClick.AddListener(QuitGame);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(RestartGame);
            quitButton.onClick.RemoveListener(QuitGame);
        }

        public void SetController(UIGameOverMenuController controller) => this.controller = controller;
        public void ShowUI() => this.gameObject.SetActive(true);
        public void HideUI() => this.gameObject.SetActive(false);
        private void RestartGame() => controller.OnRestartGame();
        private void QuitGame() => controller.OnQuitGame();
        public void OnGameOver(int finalScore, int highScore)
        {
            scoreText.text = "Score: " + finalScore;
            highscoreText.text = "Highscore: " + highScore;
        }
    }
}
