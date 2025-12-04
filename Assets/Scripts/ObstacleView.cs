using UnityEngine;
using EndlessRunner.Obstacle;     // <-- REQUIRED so ObstacleController is visible
using EndlessRunner.Player;       // For IPlayer interface
using EndlessRunner.Common;

namespace EndlessRunner.Obstacle   // <-- MUST MATCH ObstacleController namespace
{
    public class ObstacleView : MonoBehaviour
    {
        private ObstacleController obstacleController;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void SetController(ObstacleController controller)
        {
            obstacleController = controller;
        }

        public void InitializeView()
        {
            gameObject.SetActive(true);
        }

        public void ResetView()
        {
            gameObject.SetActive(false);
        }

        public void SetRandomSprite(Sprite[] sprites)
        {
            if (sprites.Length > 0)
                spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }

        private void Update()
        {
            if (obstacleController != null)
            {
                transform.Translate(obstacleController.GetVelocity() * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Despawn"))
            {
                obstacleController.Deactivate();
                obstacleController.OnObstacleAvoided();
                return;
            }

            // Player collision
            IPlayer player = collision.GetComponentInParent<IPlayer>();
            if (player != null)
            {
                player.OnHitByObstacle();

                // RL death notification
                var mb = collision.GetComponentInParent<PlayerControllerVoiceMB>();
                mb?.NotifyDeath();
            }
        }
    }
}
