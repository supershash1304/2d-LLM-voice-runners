using UnityEngine;
using EndlessRunner.Data;

namespace EndlessRunner.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D playerRB;
        [SerializeField] private Transform feetPosition;

        private PlayerController controller;
        private LayerMask groundLayer;
        private float groundDistance;

        private bool jumpRequested = false;
        private float jumpForce;

        public void InitializeView(PlayerData data, PlayerController ctrl)
        {
            controller = ctrl;
            groundLayer = data.GroundLayer;
            groundDistance = data.GroundDistance;
            playerRB.gravityScale = data.GravityScale;
        }

        public bool CheckIfGrounded()
        {
            return Physics2D.OverlapCircle(feetPosition.position, groundDistance, groundLayer);
        }

        public void RequestJump(float force)
        {
            jumpRequested = true;
            jumpForce = force;
        }

        private void FixedUpdate()
        {
            if (jumpRequested)
            {
                playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, jumpForce);
                jumpRequested = false;
            }
        }

        public void ResetPhysicsState()
        {
            playerRB.linearVelocity = Vector2.zero;
            playerRB.angularVelocity = 0f;
        }

        public void OnHitByObstacle()
        {
            controller.OnHitByObstacle();
        }
    }
}
