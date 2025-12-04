using EndlessRunner.Data;
using UnityEngine;

namespace EndlessRunner.Player
{
    public class PlayerController
    {
        protected PlayerData playerData;
        protected PlayerManager manager;

        protected PlayerModel model;
        protected PlayerView view;

        public PlayerController(PlayerData data, PlayerManager manager)
        {
            this.playerData = data;
            this.manager = manager;
        }

        public virtual void InitializeController()
        {
            model = new PlayerModel(playerData, this);
            view = Object.Instantiate(playerData.Player1Prefab, playerData.Player1SpawnPosition, Quaternion.identity);
            view.InitializeView(playerData, this);
            model.InitializeModel();
        }

        public virtual void OnUpdate(float dt)
        {
            bool grounded = view.CheckIfGrounded();
            model.SetIsGrounded(grounded);

            model.ProcessInput(dt);

            if (model.ShouldApplyJump)
                view.RequestJump(model.GetJumpForce);
        }

        public virtual void OnObstacleAvoided(int scoreValue)
        {
            model.IncreaseScore(scoreValue);
            manager.OnScoreUpdated(model.GetScore());
        }

        // 🔥 MUST BE VIRTUAL
        public virtual void OnHitByObstacle()
        {
            manager.OnHitByObstacle();
        }

        public virtual void OnGameOver()
        {
            if (view != null)
                Object.Destroy(view.gameObject);
        }
    }
}
