using EndlessRunner.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace EndlessRunner.Obstacle
{
    public class ObstaclePool
    {
        private ObstacleData obstacleData;
        private ObstacleManager obstacleManager;

        private ObjectPool<ObstacleController> obstacleControllers;
        private List<ObstacleController> activeObstacles = new List<ObstacleController>();

        public ObstaclePool(ObstacleData obstacleData, ObstacleManager obstacleManager)
        {
            this.obstacleData = obstacleData;
            this.obstacleManager = obstacleManager;
        }

        public void InitializePool()
        {
            obstacleControllers = new ObjectPool<ObstacleController>(CreateController,
                                                                     OnGetController,
                                                                     OnReleaseController,
                                                                     OnDestroyController);
        }

        private ObstacleController CreateController()
        {
            ObstacleController obstacleController = new ObstacleController(obstacleData, obstacleManager);
            obstacleController.CreateModel();
            obstacleController.CreateView();
            return obstacleController;
        }

        private void OnGetController(ObstacleController obstacleController)
        {
            obstacleController.InitializeController();
            activeObstacles.Add(obstacleController);
        }

        private void OnReleaseController(ObstacleController obstacleController)
        {
            obstacleController.ResetController();
            activeObstacles.Remove(obstacleController);
        }

        private void OnDestroyController(ObstacleController obstacleController)
        {
            //
        }

        public ObstacleController GetObstacle() => obstacleControllers.Get();
        public void ReleaseObstacle(ObstacleController controller) => obstacleControllers.Release(controller);

        public void ReleaseAllActiveObstacles()
        {
            foreach (ObstacleController controller in activeObstacles.ToArray())
            {
                controller.Deactivate();
            }
        }
    }
}







