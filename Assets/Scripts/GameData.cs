using EndlessRunner.Game;
using EndlessRunner.Obstacle;
using EndlessRunner.Parallax;
using EndlessRunner.Player;
using EndlessRunner.UI;
using UnityEngine;

namespace EndlessRunner.Data
{
    [CreateAssetMenu(menuName = "Game Data/ Game Data", fileName = "Game Data")]
    public class GameData : ScriptableObject
    {
        [Header("Prefabs")]
        [SerializeField] private GameManager gameManagerPrefab;
        [SerializeField] private UIManager uiManagerPrefab;
        [SerializeField] private PlayerManager playerManagerPrefab;
        [SerializeField] private ObstacleManager obstacleManagerPrefab;
        [SerializeField] private ParallaxManager parallaxManagerPrefab;

        public GameManager GameManagerPrefab => gameManagerPrefab;
        public UIManager UIManagerPrefab => uiManagerPrefab;
        public PlayerManager PlayerManagerPrefab => playerManagerPrefab;
        public ObstacleManager ObstacleManagerPrefab => obstacleManagerPrefab;
        public ParallaxManager ParallaxManagerPrefab => parallaxManagerPrefab;
    }
}