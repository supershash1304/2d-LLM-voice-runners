using EndlessRunner.Data;
using UnityEngine;

namespace EndlessRunner.Player
{
    public class PlayerModelVoice
    {
        private PlayerControllerVoice controller;
        private PlayerData data;

        private bool isGrounded;
        private bool applyJump;

        public bool ExternalJumpRequested = false;
        public float JumpMultiplier = 1f;

        public PlayerModelVoice(PlayerData data, PlayerControllerVoice controller)
        {
            this.data = data;
            this.controller = controller;
        }

        public void InitializeModel()
        {
            isGrounded = true;
            applyJump = false;
        }

        public void SetIsGrounded(bool g)
        {
            isGrounded = g;
        }

        public void ProcessExternalInput(float dt)
        {
            applyJump = false;

            // Only jump if an EXTERNAL trigger was sent (RL or voice)
            if (ExternalJumpRequested && isGrounded)
            {
                applyJump = true;
                ExternalJumpRequested = false; // Consume input
            }
        }

        public bool ShouldApplyJump => applyJump;

        public float GetJumpForce => data.JumpForce * JumpMultiplier;
    }
}
