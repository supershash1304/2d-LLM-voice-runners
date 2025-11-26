using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float speed = 0.2f;
    public Transform player;

    void Update()
    {
        transform.position = new Vector3(
            player.position.x * speed,
            transform.position.y,
            transform.position.z
        );
    }
}
