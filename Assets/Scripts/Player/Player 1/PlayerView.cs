using UnityEngine;
using EndlessRunner.Data;
using EndlessRunner.Common;

namespace EndlessRunner.Player
{
    public class PlayerView : MonoBehaviour, IPlayer
    {
        [SerializeField] private Rigidbody2D playerRB;
        [SerializeField] private Transform feetPosition;

        private PlayerController controller;
        private LayerMask groundLayer;
        private float groundDistance;

        private bool jumpRequested;
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
            return Physics2D.OverlapCircle(
                feetPosition.position,
                groundDistance,
                groundLayer
            );
        }

        public void RequestJump(float force)
        {
            jumpRequested = true;
            jumpForce = force;
        }

        private void FixedUpdate()
        {
            if (!jumpRequested)
                return;
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, 0f);
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpRequested = false;
        }

        public void ResetPhysicsState()
        {
            playerRB.linearVelocity = Vector2.zero;
            playerRB.angularVelocity = 0f;
        }
       public void OnHitByObstacle()
{
    if (controller == null)
    {
        // This is Player2 (voice / RL player)
        Debug.Log("[PlayerView] Hit detected for RL Player — forwarding to PlayerManager");

        var manager = FindAnyObjectByType<EndlessRunner.Player.PlayerManager>();
        if (manager != null)
            manager.OnHitByObstacle();

        return;
    }

    controller.OnHitByObstacle();
}

    }
}
