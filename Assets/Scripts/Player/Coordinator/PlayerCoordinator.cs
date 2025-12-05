using EndlessRunner.Common;
using EndlessRunner.Data;
using EndlessRunner.Event;
using UnityEngine;
using System.IO;

namespace EndlessRunner.Player
{
    public class PlayerManager : MonoBehaviour, IManager
    {
        [SerializeField] private PlayerData playerData;

        private IEventManager eventManager;
        private GameState currentGameState;

        private PlayerController player1;            // Manual runner
        private PlayerControllerVoice player2;       // Voice/RL runner
        private PlayerControllerVoiceMB player2MB;   // MonoBehaviour wrapper

        private int currentScore;
        private int highScore;

        private string ScoreFilePath =>
            Path.Combine(Application.persistentDataPath, "scoreData.json");

        public PlayerControllerVoiceMB VoicePlayerMB => player2MB;
        public GameState CurrentGameState => currentGameState;

        public void InitializeManager(IEventManager eventManager)
        {
            this.eventManager = eventManager;
            RegisterEventListeners();
            LoadHighScoreFromJson();
        }

        private void RegisterEventListeners()
        {
            eventManager.GameEvents.OnGameStateUpdated.AddListener(OnGameStateUpdated);
            eventManager.ObstacleEvents.OnObstacleAvoided.AddListener(OnObstacleAvoided);
        }

        private void OnGameStateUpdated(GameState newState)
        {
            currentGameState = newState;

            switch (newState)
            {
                case GameState.IN_GAME:
                    OnGameStart();
                    break;

                case GameState.GAME_OVER:
                    OnGameOver();
                    break;
            }
        }

        private void Update()
        {
            if (currentGameState != GameState.IN_GAME)
                return;

            player1?.OnUpdate(Time.deltaTime);
            player2?.OnUpdate(Time.deltaTime);
        }

        private void OnGameStart()
        {
            // --- PLAYER 1 (manual) ---
            if (player1 == null)
            {
                player1 = new PlayerController(playerData, this);
                player1.InitializeController();
            }

            // --- PLAYER 2 (voice + RL, inference-only) ---
            if (player2 == null)
            {
                player2 = new PlayerControllerVoice(playerData, this);
                player2.InitializeController();
                player2MB = player2.voiceMB;
            }
        }

        private void OnObstacleAvoided(int scoreValue)
        {
            player1?.OnObstacleAvoided(scoreValue);
            // You can forward to player2 as well if you want separate scoring
        }

        public void OnScoreUpdated(int newScore)
        {
            currentScore = newScore;
            eventManager.PlayerEvents.OnScoreUpdated.Invoke(newScore);
        }

        public void OnHitByObstacle()
        {
            // Both players use this same game-over pipeline
            eventManager.PlayerEvents.OnHitByObstacle.Invoke();
        }

        public void OnGameOver()
        {
            player1?.OnGameOver();
            player2?.OnGameOver();

            player1 = null;
            player2 = null;
            player2MB = null;

            UpdateHighscore();
            eventManager.PlayerEvents.OnGameover.Invoke(currentScore, highScore);
        }

        private void UpdateHighscore()
        {
            if (currentScore > highScore)
            {
                highScore = currentScore;
                File.WriteAllText(
                    ScoreFilePath,
                    JsonUtility.ToJson(new PlayerScoreData { highScore = highScore })
                );
            }
        }

        private void LoadHighScoreFromJson()
        {
            if (File.Exists(ScoreFilePath))
            {
                var data = JsonUtility.FromJson<PlayerScoreData>(
                    File.ReadAllText(ScoreFilePath)
                );
                highScore = data.highScore;
            }
            else
            {
                highScore = 0;
            }
        }
    }
}
