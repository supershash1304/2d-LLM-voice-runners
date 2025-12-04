using EndlessRunner.Data;
using UnityEngine;

namespace EndlessRunner.Player
{
    public class PlayerModel
    {
        private PlayerController playerController;
        private PlayerData playerData;

        private float jumpForce;
        private float jumpTime;

        private bool isGrounded;
        private bool isJumping;
        private float jumpTimer;

        private bool applyJump;
        private int playerScore;

        public PlayerModel(PlayerData data, PlayerController controller)
        {
            playerData = data;
            playerController = controller;
        }

        public void InitializeModel()
        {
            jumpForce = playerData.JumpForce;
            jumpTime = playerData.JumpTime;
            playerScore = 0;
        }

        public void SetIsGrounded(bool grounded)
        {
            isGrounded = grounded;
            if (grounded)
            {
                isJumping = false;
                jumpTimer = 0f;
            }
        }

        // Keyboard input for Player1 only
        public void ProcessInput(float dt)
        {
            applyJump = false;

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                jumpTimer = 0f;
                applyJump = true;
            }
            else if (isJumping && Input.GetKey(KeyCode.Space))
            {
                if (jumpTimer < jumpTime)
                {
                    applyJump = true;
                    jumpTimer += dt;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
                isJumping = false;
        }

        public bool ShouldApplyJump => applyJump;
        public float GetJumpForce => jumpForce;

        public void IncreaseScore(int amount)
        {
            playerScore += amount;
        }

        public int GetScore() => playerScore;
    }
}
