using EndlessRunner.Data;
using UnityEngine;

namespace EndlessRunner.Obstacle
{
    public class ObstacleController
    {
        private ObstacleManager obstacleManager;
        private ObstacleData obstacleData;
        private ObstacleModel obstacleModel;
        private ObstacleView obstacleView;

        // RL / Spawner support
        public event System.Action<ObstacleController> OnObstacleDeactivate;
        public bool IsActive => obstacleView != null && obstacleView.gameObject.activeSelf;
        public Transform transform => obstacleView.transform;

        public ObstacleController(ObstacleData obstacleData, ObstacleManager obstacleManager)
        {
            this.obstacleData = obstacleData;
            this.obstacleManager = obstacleManager;
        }

        public void CreateModel() => obstacleModel = new ObstacleModel(obstacleData);

        public void CreateView()
        {
            obstacleView = GameObject.Instantiate<ObstacleView>(
                obstacleData.ObstacleViewPrefab,
                obstacleManager.transform
            );
        }

        public void InitializeController()
        {
            obstacleModel.InitializeModel();
            obstacleView.InitializeView();
            obstacleView.SetController(this);
        }

        public void ResetController()
        {
            obstacleModel.ResetModel();
            obstacleView.ResetView();
        }

        public void Activate(Vector3 spawnPosition)
        {
            obstacleView.transform.position = spawnPosition;
            obstacleView.SetRandomSprite(obstacleData.ObstacleSprites);
            obstacleView.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            obstacleView.gameObject.SetActive(false);

            // notify spawner for RL tracking
            OnObstacleDeactivate?.Invoke(this);

            // return to pool
            obstacleManager.GetPool().ReleaseObstacle(this);
        }

        public Vector2 GetVelocity() => obstacleModel.GetVelocity();

        public void OnObstacleAvoided()
        {
            obstacleManager.OnObstacleAvoided(obstacleModel.ScoreValue);
        }
    }
}
