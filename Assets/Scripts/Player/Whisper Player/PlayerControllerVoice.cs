using EndlessRunner.Data;
using UnityEngine;

namespace EndlessRunner.Player
{
    public class PlayerControllerVoice : PlayerController
    {
        private PlayerModelVoice modelVoice;
        private PlayerView voiceView;

        public PlayerControllerVoice(PlayerData data, PlayerManager manager)
            : base(data, manager) {}

        public override void InitializeController()
        {
            modelVoice = new PlayerModelVoice(playerData, this);

            voiceView = Object.Instantiate(playerData.Player2Prefab,
                                           playerData.Player2SpawnPosition,
                                           Quaternion.identity);

            voiceView.InitializeView(playerData, this);
            modelVoice.InitializeModel();
        }

        public override void OnUpdate(float dt)
        {
            bool grounded = voiceView.CheckIfGrounded();
            modelVoice.SetIsGrounded(grounded);

            // RL decides jump — NOT keyboard
            modelVoice.ProcessExternalInput(dt);

            if (modelVoice.ShouldApplyJump)
                voiceView.RequestJump(modelVoice.GetJumpForce);
        }

        public void TriggerJump(float jumpMult)
        {
            modelVoice.ExternalJumpRequested = true;
            modelVoice.JumpMultiplier = jumpMult;
        }

        public override void OnHitByObstacle()
        {
            base.OnHitByObstacle();

            var rl = GameObject.FindAnyObjectByType<VoiceRLAgent>();
            if (rl != null)
            {
                rl.AddReward(-1f);
                rl.EndEpisode();
            }
        }

        public void ResetForRLTraining()
        {
            if (voiceView != null)
            {
                voiceView.transform.position = playerData.Player2SpawnPosition;
                voiceView.ResetPhysicsState();
            }

            modelVoice.ExternalJumpRequested = false;
            modelVoice.JumpMultiplier = 1f;
            modelVoice.SetIsGrounded(true);
        }
        public void RewardSuccess()
{
    var rl = GameObject.FindAnyObjectByType<VoiceRLAgent>();
    if (rl != null)
    {
        rl.AddReward(+0.5f);   // reward for success
    }
}

        public PlayerView GetView() => voiceView;
    }
}
