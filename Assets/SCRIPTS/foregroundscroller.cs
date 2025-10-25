using UnityEngine;

public class ForegroundScroller : MonoBehaviour
{
    public Transform player;
    public float scrollFactor = 0.6f;
    private float startX;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) player = p.transform;
            else { Debug.LogError("No player found!"); enabled = false; return; }
        }

        startX = transform.position.x;
    }

    void Update()
    {
        if (player == null) return;
        float offset = player.position.x * scrollFactor;
        transform.position = new Vector3(startX + offset, transform.position.y, transform.position.z);
    }
}
