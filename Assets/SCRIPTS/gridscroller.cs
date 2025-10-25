using UnityEngine;

public class GridFollower : MonoBehaviour
{
    public Transform player;
    public float offsetX = 0f;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) player = p.transform;
            else { Debug.LogError("No player found!"); enabled = false; return; }
        }
    }

    void LateUpdate()
    {
        if (player == null) return;
        transform.position = new Vector3(player.position.x + offsetX, transform.position.y, transform.position.z);
    }
}
