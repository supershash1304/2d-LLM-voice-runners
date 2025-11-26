using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;
    public float minDist = 10f;
    public float maxDist = 20f;

    private float nextX;

    void Start()
    {
        nextX = player.position.x + 15f;
    }

    void Update()
    {
        if (player.position.x > nextX - 25)
        {
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        float x = nextX;
        float y = transform.position.y;

        Instantiate(obstaclePrefab, new Vector3(x, y, 0), Quaternion.identity);

        nextX += Random.Range(minDist, maxDist);
    }
}
