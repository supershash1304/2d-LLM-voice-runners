using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;  // Player transform (drag here or tag "Player")

    [Header("Layers")]
    public Transform backgroundLayer;
    public Transform foregroundLayer;
    public Transform gridLayer;

    [Header("Scroll Factors")]
    [Tooltip("Smaller = slower parallax (background). 1 = same as player (grid).")]
    public float backgroundFactor = 0.3f;
    public float foregroundFactor = 0.6f;
    public float gridFactor = 1f;

    private float lastPlayerX;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) player = p.transform;
            else
            {
                Debug.LogError("ParallaxManager: No player assigned or found with tag 'Player'");
                enabled = false;
                return;
            }
        }

        lastPlayerX = player.position.x;
    }

    void LateUpdate()
    {
        if (player == null) return;

        float deltaX = player.position.x - lastPlayerX;

        if (Mathf.Abs(deltaX) > 0.0001f)
        {
            // move each layer only when player moves
            if (backgroundLayer)
                backgroundLayer.position += Vector3.right * deltaX * backgroundFactor;

            if (foregroundLayer)
                foregroundLayer.position += Vector3.right * deltaX * foregroundFactor;

            if (gridLayer)
                gridLayer.position += Vector3.right * deltaX * gridFactor;
        }

        lastPlayerX = player.position.x;
    }
}
