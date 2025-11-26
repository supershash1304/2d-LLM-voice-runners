using UnityEngine;

public class GroundTriggerSpawner : MonoBehaviour
{
    public GameObject[] groundPrefabs;      // 3 prefabs
    public Transform lastGround;            // last ground in scene

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        SpawnGround();
    }

    void SpawnGround()
    {
        // pick random prefab
        GameObject prefab = groundPrefabs[Random.Range(0, groundPrefabs.Length)];

        // get width
        float width = prefab.GetComponent<SpriteRenderer>().bounds.size.x;

        // spawn position at END of last ground
        Vector3 spawnPos = new Vector3(
            lastGround.position.x + width,
            lastGround.position.y,
            0
        );

        // create tile
        GameObject newGround = Instantiate(prefab, spawnPos, Quaternion.identity);

        // move spawner so next spawn happens later
        transform.position = new Vector3(
            spawnPos.x + width / 2f,
            transform.position.y,
            transform.position.z
        );

        // update last ground
        lastGround = newGround.transform;
    }
}
