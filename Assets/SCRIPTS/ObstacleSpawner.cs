using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunner.Obstacle
{
    public class ObstacleSpawner
    {
        private ObstacleManager obstacleManager;
        private Coroutine spawnRoutine;

        // RL support: track active obstacles
        private readonly List<ObstacleController> activeObstacles = new List<ObstacleController>();

        public ObstacleSpawner(ObstacleManager obstacleManager)
        {
            this.obstacleManager = obstacleManager;
        }

        public void StartSpawning()
        {
            spawnRoutine = obstacleManager.StartCoroutine(StartSpawner());
        }

        public void StopSpawning()
        {
            if (spawnRoutine != null)
                obstacleManager.StopCoroutine(spawnRoutine);
        }

        private IEnumerator StartSpawner()
        {
            while (true)
            {
                yield return new WaitForSeconds(obstacleManager.GetData().ObstacleSpawnTime);

                ObstacleController controller = obstacleManager.GetPool().GetObstacle();
                if (controller != null)
                {
                    Vector3[] spawnPositions = obstacleManager.GetData().SpawnPositions;
                    Vector3 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)];

                    controller.Activate(spawnPos);
                    RegisterObstacle(controller);
                }
            }
        }

        // RL tracking
        public void RegisterObstacle(ObstacleController obstacle)
        {
            if (!activeObstacles.Contains(obstacle))
            {
                activeObstacles.Add(obstacle);
                obstacle.OnObstacleDeactivate += UnregisterObstacle;
            }
        }

        public void UnregisterObstacle(ObstacleController obstacle)
        {
            if (activeObstacles.Contains(obstacle))
            {
                activeObstacles.Remove(obstacle);
                obstacle.OnObstacleDeactivate -= UnregisterObstacle;
            }
        }

        // Optional: distance to next obstacle (if you want to feed RL)
        public float GetDistanceToNextObstacle(float playerX)
        {
            float closest = float.MaxValue;

            foreach (var obstacle in activeObstacles)
            {
                if (obstacle == null || !obstacle.IsActive)
                    continue;

                float dx = obstacle.transform.position.x - playerX;
                if (dx > 0f && dx < closest)
                    closest = dx;
            }

            return closest == float.MaxValue ? 20f : closest;
        }
    }
}
