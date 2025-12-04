using UnityEngine;
using System.Collections.Generic;

public class TrainingEnvironment : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private GameObject spawned;

    public float CurrentDistance { get; private set; }
    public float CurrentHeight { get; private set; }

    public Transform player;

    public void ResetEnvironment()
    {
        if (spawned != null)
            Destroy(spawned);

        float height = Random.Range(1f, 4f);
        float distance = Random.Range(5f, 12f);

        spawned = Instantiate(obstaclePrefab, new Vector3(distance, 0, 0), Quaternion.identity);
        spawned.transform.localScale = new Vector3(1f, height, 1f);

        CurrentDistance = distance - player.position.x;
        CurrentHeight = height;
    }

    void Update()
    {
        if (spawned != null)
            CurrentDistance = spawned.transform.position.x - player.position.x;
    }
}
