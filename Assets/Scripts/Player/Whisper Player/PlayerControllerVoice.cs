using UnityEngine;
using EndlessRunner.Data;

namespace EndlessRunner.Player
{
    public class PlayerControllerVoice : PlayerController
    {
        private PlayerView voiceView;
        public PlayerControllerVoiceMB VoiceMB { get; private set; }
        public PlayerControllerVoice(PlayerData data, PlayerManager mgr)
            : base(data, mgr) { }
        public override void InitializeController()
        {
            
            voiceView = Object.Instantiate(
                playerData.Player2Prefab,
                playerData.Player2SpawnPosition,
                Quaternion.identity
            );

            
            voiceView.InitializeView(playerData, this);

           
            var mb = voiceView.GetComponent<PlayerControllerVoiceMB>();
            if (mb == null)
            {
                mb = voiceView.gameObject.AddComponent<PlayerControllerVoiceMB>();
                Debug.LogWarning("[P2] Added PlayerControllerVoiceMB at runtime.");
            }

            
            VoiceMB = mb;

            
            var rl = Object.FindAnyObjectByType<VoiceRLAgent>();
            if (rl != null)
            {
                rl.voiceWrapper = mb;
                Debug.Log("[P2] RL Agent linked to PlayerControllerVoiceMB!");
            }
        }

        public override void OnUpdate(float dt)
        {
            
        }

        public void TriggerJump(float multiplier)
        {
            
        }

        public void ResetForRLTraining()
        {
            
        }
    }
}
