using EndlessRunner.Common;
using UnityEngine;

namespace EndlessRunner.Obstacle
{
    public class ObstacleView : MonoBehaviour
    {
        private ObstacleController obstacleController;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void SetController(ObstacleController obstacleController)
        {
            this.obstacleController = obstacleController;
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
                transform.Translate(obstacleController.GetVelocity() * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("[OBSTACLE] Hit: " + collision.name);

            if (collision.CompareTag("Despawn"))
            {
                obstacleController.Deactivate();
                obstacleController.OnObstacleAvoided();
                return;
            }

            IPlayer player = collision.GetComponentInParent<IPlayer>();
            if (player != null)
            {
                Debug.Log("[OBSTACLE] Player detected via IPlayer: " + player);
                player.OnHitByObstacle();
            }
        }
    }
}
