using UnityEngine;

namespace EndlessRunner.Player
{
    public class PlayerControllerVoiceMB : MonoBehaviour
    {
        public PlayerControllerVoice Controller { get; private set; }
        public PlayerView View { get; private set; }

        // Compatibility
        public PlayerControllerVoice controller => Controller;

        public void Init(PlayerControllerVoice controller, PlayerView view)
        {
            Controller = controller;
            View = view;
        }

        public void TriggerJump(float multiplier)
        {
            Controller?.TriggerJump(multiplier);
        }

        public void ResetForRLTraining()
        {
            Controller?.ResetForRLTraining();
        }

        // 🔥 Add this method — required by ObstacleView
        public void NotifyDeath()
        {
            // Reward the RL Agent if needed
            var rl = FindObjectOfType<VoiceRLAgent>();
            if (rl != null)
            {
                rl.AddReward(-1f);   // punishment for death
                rl.EndEpisode();
            }

            // Forward to controller so normal game logic works
            Controller?.OnHitByObstacle();
        }
    }
}
