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

        private PlayerController player1;
        private PlayerControllerVoice player2;
        private PlayerControllerVoiceMB player2MB;

        private int currentScore;
        private int highScore;

        private string ScoreFilePath => Path.Combine(Application.persistentDataPath, "scoreData.json");

        public PlayerControllerVoice VoicePlayer => player2;
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
            if (currentGameState == GameState.IN_GAME)
            {
                player1?.OnUpdate(Time.deltaTime);
                player2?.OnUpdate(Time.deltaTime);
            }
        }

        private void OnGameStart()
        {
            if (player1 == null)
            {
                player1 = new PlayerController(playerData, this);
                player1.InitializeController();
            }

            // inside OnGameStart()
if (player2 == null)
{
    player2 = new PlayerControllerVoice(playerData, this);
    player2.InitializeController();

    var view = player2.GetView();
    var mb = view.gameObject.AddComponent<PlayerControllerVoiceMB>();
    mb.Init(player2, view);

    var rl = Object.FindAnyObjectByType<VoiceRLAgent>();
    if (rl != null)
        rl.voiceWrapper = mb;
}

        }

        private void OnObstacleAvoided(int scoreValue)
        {
            player1?.OnObstacleAvoided(scoreValue);
        }

        public void OnScoreUpdated(int newScore)
        {
            currentScore = newScore;
            eventManager.PlayerEvents.OnScoreUpdated.Invoke(newScore);
        }

        public void OnHitByObstacle()
        {
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
                File.WriteAllText(ScoreFilePath, JsonUtility.ToJson(new PlayerScoreData { highScore = highScore }));
            }
        }

        private void LoadHighScoreFromJson()
        {
            if (File.Exists(ScoreFilePath))
            {
                highScore = JsonUtility.FromJson<PlayerScoreData>(File.ReadAllText(ScoreFilePath)).highScore;
            }
            else highScore = 0;
        }
    }
}
