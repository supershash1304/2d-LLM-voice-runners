using UnityEngine;
using EndlessRunner.Obstacle;

namespace EndlessRunner.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerView))]
    public class PlayerControllerVoiceMB : MonoBehaviour
    {
        public PlayerView View { get; private set; }

        private Rigidbody2D rb;
        private Vector3 initialPosition;

        public float baseJumpForce = 30f;
        public float minMultiplier = 0.3f;
        public float maxMultiplier = 1.8f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            View = GetComponent<PlayerView>();
            initialPosition = transform.position;
        }

        // 🔥 CALLED BY RL AGENT
       public void TriggerJump(float multiplier)
{
    Debug.Log($"[MB] TriggerJump! multiplier={multiplier}");

    if (!View.CheckIfGrounded())
    {
        Debug.Log("[MB] Not grounded — no jump.");
        return;
    }

    multiplier = Mathf.Clamp(multiplier, minMultiplier, maxMultiplier);

    float finalForce = baseJumpForce * multiplier * 3.5f; // BOOST multiplier

    Debug.Log("[MB] Applying velocity jump: " + finalForce);

    // Same jump method as Player1
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, finalForce);
}


        public void ResetForRLTraining()
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = initialPosition;
        }

        private void Update()
        {
            var rl = FindAnyObjectByType<VoiceRLAgent>();
            if (rl == null) return;

            float dist = CalculateDistanceToNextObstacle();
            float height = transform.position.y;
            bool grounded = View.CheckIfGrounded();

            rl.SetEnvironment(dist, height, grounded);
        }

        private float CalculateDistanceToNextObstacle()
        {
            float nearest = float.MaxValue;

            var obstacles = FindObjectsByType<ObstacleView>(FindObjectsSortMode.None);

            foreach (var o in obstacles)
            {
                float diff = o.transform.position.x - transform.position.x;
                if (diff > 0 && diff < nearest)
                    nearest = diff;
            }

            return nearest == float.MaxValue ? 10f : nearest;
        }
    }
}
