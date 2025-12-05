using UnityEngine;
using EndlessRunner.Obstacle;

namespace EndlessRunner.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerView))]
    public class PlayerControllerVoiceMB : MonoBehaviour
    {
        public PlayerView View { get; set; }

        private Rigidbody2D rb;
        private Vector3 initialPosition;

        public float baseJumpForce = 50f;
        public float minMultiplier = 1f;
        public float maxMultiplier = 3f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            if (View == null)
                View = GetComponent<PlayerView>();

            initialPosition = transform.position;
        }

        public void TriggerJump(float multiplier)
{
    if (View == null)
    {
        Debug.LogWarning("[VoiceMB] View is NULL, cannot jump.");
        return;
    }

    if (!View.CheckIfGrounded())
        return;

    multiplier = Mathf.Clamp(multiplier, minMultiplier, maxMultiplier);
    float finalForce = baseJumpForce * multiplier * 3f;

    Debug.Log($"[VoiceMB] TriggerJump multiplier={multiplier}, finalForce={finalForce}");

    // RESET existing vertical velocity
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

   
    rb.AddForce(Vector2.up * finalForce, ForceMode2D.Impulse);
}


        public void ResetForRLTraining()
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = initialPosition;
        }

        private void Update()
        {
            var rl = GetComponent<VoiceRLAgent>();
            if (rl == null || View == null)
                return;

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
