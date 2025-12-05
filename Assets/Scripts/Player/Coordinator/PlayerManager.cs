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
        private PlayerControllerVoiceMB player2MB;
        private VoiceRLAgent rlAgent;

        private int currentScore;
        private int highScore;

        private string ScoreFilePath => Path.Combine(Application.persistentDataPath, "scoreData.json");

        public PlayerControllerVoiceMB VoicePlayerMB => player2MB;
        public GameState CurrentGameState => currentGameState;

        // INITIALIZATION

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

        // GAME STATE HANDLING

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
            }
        }

        // GAME START

    private void OnGameStart()
        {
            // ------- PLAYER 1 -------
            if (player1 == null)
            {
                player1 = new PlayerController(playerData, this);
                player1.InitializeController();
            }

            // ------- PLAYER 2 (VOICE + RL) -------
            // --- PLAYER 2 (VOICE + RL) ---
        if (player2MB == null)
        {
    
            var viewObj = Object.Instantiate(
            playerData.Player2Prefab,
            playerData.Player2SpawnPosition,
            Quaternion.identity
            );

    
    PlayerView pView = viewObj.GetComponent<PlayerView>();
    if (pView == null)
    {
        Debug.LogError("Player2 prefab has NO PlayerView!");
        return;
    }

   
    pView.InitializeView(playerData, null);

    
    player2MB = viewObj.GetComponent<PlayerControllerVoiceMB>();
    if (player2MB == null)
    {
        player2MB = viewObj.gameObject.AddComponent<PlayerControllerVoiceMB>();
        Debug.LogWarning("[PlayerManager] Added missing PlayerControllerVoiceMB");
    }

    
    player2MB.View = pView;

   
    rlAgent = viewObj.GetComponent<VoiceRLAgent>();
    if (rlAgent == null)
    {
        rlAgent = viewObj.gameObject.AddComponent<VoiceRLAgent>();
        Debug.Log("[PlayerManager] Added VoiceRLAgent");
    }

    
    rlAgent.voiceWrapper = player2MB;
}
}


        // SCORING + HIT EVENTS
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

        // GAME OVER
        public void OnGameOver()
        {
            // Player1 destroyed
            player1?.OnGameOver();
            player1 = null;

            // Player2 destroyed
            if (player2MB != null)
            {
                Destroy(player2MB.gameObject);
                player2MB = null;
            }

            if (rlAgent != null)
            {
                Destroy(rlAgent.gameObject);
                rlAgent = null;
            }

            UpdateHighscore();
            eventManager.PlayerEvents.OnGameover.Invoke(currentScore, highScore);
        }



        // HIGHSCORE SAVE/LOAD
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
                highScore = JsonUtility.FromJson<PlayerScoreData>(
                    File.ReadAllText(ScoreFilePath)
                ).highScore;
            }
            else
            {
                highScore = 0;
            }
        }
    }
}
