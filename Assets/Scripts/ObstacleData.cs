using EndlessRunner.Obstacle;
using UnityEngine;

namespace EndlessRunner.Data
{
    [CreateAssetMenu(menuName = "Game Data/ Obstacle Data", fileName = "Obstacle Data")]
    public class ObstacleData : ScriptableObject
    {
        [SerializeField] private ObstacleView obstacleViewPrefab;
        [SerializeField] private Sprite[] obstacleSprites;
        [SerializeField] private Vector3[] spawnPositions;
        [SerializeField] private float obstacleSpawnTime;
        [SerializeField] private float moveSpeed;
        [SerializeField] private int scoreValue;

        public ObstacleView ObstacleViewPrefab => obstacleViewPrefab;
        public Sprite[] ObstacleSprites => obstacleSprites;
        public Vector3[] SpawnPositions => spawnPositions;
        public float ObstacleSpawnTime => obstacleSpawnTime;
        public float MoveSpeed => moveSpeed;
        public int ScoreValue => scoreValue;
    }
}