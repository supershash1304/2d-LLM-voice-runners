using UnityEngine;

public class ObstacleTracker : MonoBehaviour
{
    public static float Distance;
    public static float Height;

    public Transform player;
    public Transform obstacle;

    void Update()
    {
        if (player != null && obstacle != null)
        {
            Distance = obstacle.position.x - player.position.x;
            Height = obstacle.localScale.y;
        }
    }
}
