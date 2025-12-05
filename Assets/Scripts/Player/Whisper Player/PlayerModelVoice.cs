using EndlessRunner.Data;

namespace EndlessRunner.Player
{
    public class PlayerModelVoice
    {
        private readonly PlayerData playerData;
        private readonly PlayerControllerVoice controller;

        private float baseJumpForce;
        private bool isGrounded;
        private bool applyJump;

        public bool ExternalJumpRequested { get; set; }
        public float JumpMultiplier { get; set; } = 1f;

        public PlayerModelVoice(PlayerData data, PlayerControllerVoice controller)
        {
            this.playerData = data;
            this.controller = controller;
        }

        public void InitializeModel()
        {
            // Use PlayerData jump force as base, or a tuned value
            baseJumpForce = playerData.JumpForce;
        }

        public void SetIsGrounded(bool grounded)
        {
            isGrounded = grounded;
        }

        public void ProcessExternalInput(float deltaTime)
        {
            applyJump = false;

            if (ExternalJumpRequested && isGrounded)
            {
                applyJump = true;
                ExternalJumpRequested = false;
            }
        }

        public float GetJumpForce => baseJumpForce * JumpMultiplier;
        public bool ShouldApplyJump => applyJump;
    }
}
