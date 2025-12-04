using UnityEngine;

namespace EndlessRunner.Player
{
    public class RewardTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            var view = col.GetComponent<PlayerView>();
            if (view == null) return;

            var wrapper = view.GetComponent<PlayerControllerVoiceMB>();
            if (wrapper == null) return;

            var controller = wrapper.Controller;
            if (controller == null) return;

            float obstacleHeight = transform.position.y;
            float playerHeight = col.transform.position.y;

            bool playerJumped = playerHeight > 0.5f;  // adjust as needed

            // Ground obstacle (small height)
            if (obstacleHeight < 1.0f)
            {
                if (playerJumped)
                    controller.RewardSuccess(); // Correct
                else
                {
                    controller.OnHitByObstacle(); // Wrong
                }
            }
            else // High obstacle
            {
                if (!playerJumped)
                    controller.RewardSuccess(); // Correct stay low
                else
                    controller.OnHitByObstacle(); // Wrong (jumped into it)
            }
        }
    }
}
