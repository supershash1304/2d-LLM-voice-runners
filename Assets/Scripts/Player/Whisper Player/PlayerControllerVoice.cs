using UnityEngine;
using EndlessRunner.Data;

namespace EndlessRunner.Player
{
    public class PlayerControllerVoice : PlayerController
    {
        private PlayerView voiceView;

        // ⭐ Required for PlayerCoordinator
        public PlayerControllerVoiceMB VoiceMB { get; private set; }

        public PlayerControllerVoice(PlayerData data, PlayerManager mgr)
            : base(data, mgr) { }

        public override void InitializeController()
        {
            // Spawn Player2 object
            voiceView = Object.Instantiate(
                playerData.Player2Prefab,
                playerData.Player2SpawnPosition,
                Quaternion.identity
            );

            // Initialize view (assign data)
            voiceView.InitializeView(playerData, this);

            // MB is already on prefab — OR add if missing
            var mb = voiceView.GetComponent<PlayerControllerVoiceMB>();
            if (mb == null)
            {
                mb = voiceView.gameObject.AddComponent<PlayerControllerVoiceMB>();
                Debug.LogWarning("[P2] Added PlayerControllerVoiceMB at runtime.");
            }

            // ⭐ Store reference so PlayerCoordinator can access it
            VoiceMB = mb;

            // Link RL Agent
            var rl = Object.FindAnyObjectByType<VoiceRLAgent>();
            if (rl != null)
            {
                rl.voiceWrapper = mb;
                Debug.Log("[P2] RL Agent linked to PlayerControllerVoiceMB!");
            }
        }

        public override void OnUpdate(float dt)
        {
            // Player2 is driven by RL & voice; no manual movement
        }

        public void TriggerJump(float multiplier)
        {
            // PlayerModelVoice removed — jump handled by MB
        }

        public void ResetForRLTraining()
        {
            // Reset handled inside MB
        }
    }
}
