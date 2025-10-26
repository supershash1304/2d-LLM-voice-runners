using UnityEngine;
using System.Collections.Generic;

public class ParallaxManager : MonoBehaviour
{
    [Header("Player Tracking")]
    public Transform player; // The player transform to follow

    [Header("Parallax Settings")]
    [Tooltip("Each layer's parallax multiplier (smaller = slower movement). Lower layers = farther away.")]
    public List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    [Header("Tile Generation Settings")]
    public List<GameObject> groundTilePrefabs;  // Prefabs for ground tiles
    public float tileLength = 20f;              // Length of each ground tile
    public int startTileCount = 5;              // How many tiles to spawn initially
    public float spawnOffset = 10f;             // Distance ahead of player to spawn next tile

    private float nextSpawnX;                   // X position to spawn next tile
    private List<GameObject> spawnedTiles = new List<GameObject>();

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("⚠️ ParallaxManager: Player transform not assigned!");
            enabled = false;
            return;
        }

        // Spawn initial tiles
        for (int i = 0; i < startTileCount; i++)
        {
            SpawnTile();
        }

        nextSpawnX = startTileCount * tileLength;
    }

    void Update()
    {
        HandleParallax();
        HandleTileSpawning();
    }

    void HandleParallax()
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            if (layer.layerTransform == null) continue;

            // Move background relative to player
            float deltaX = (player.position.x - layer.startPosX) * layer.parallaxMultiplier;
            layer.layerTransform.position = new Vector3(layer.startPosX + deltaX, layer.layerTransform.position.y, layer.layerTransform.position.z);
        }
    }

    void HandleTileSpawning()
    {
        // Spawn new tiles when player approaches next spawn position
        if (player.position.x + spawnOffset >= nextSpawnX)
        {
            SpawnTile();
            nextSpawnX += tileLength;

            // Clean up old tiles to avoid memory buildup
            if (spawnedTiles.Count > startTileCount + 2)
            {
                Destroy(spawnedTiles[0]);
                spawnedTiles.RemoveAt(0);
            }
        }
    }

    void SpawnTile()
    {
        if (groundTilePrefabs.Count == 0) return;

        GameObject prefab = groundTilePrefabs[Random.Range(0, groundTilePrefabs.Count)];
        Vector3 spawnPos = new Vector3(spawnedTiles.Count * tileLength, 0f, 0f);

        GameObject newTile = Instantiate(prefab, spawnPos, Quaternion.identity);
        spawnedTiles.Add(newTile);
    }
}

[System.Serializable]
public class ParallaxLayer
{
    public Transform layerTransform;
    [Range(0f, 1f)] public float parallaxMultiplier = 0.5f;
    [HideInInspector] public float startPosX;

    public void Initialize()
    {
        if (layerTransform != null)
            startPosX = layerTransform.position.x;
    }
}
