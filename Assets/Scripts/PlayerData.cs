using EndlessRunner.Player;
using UnityEngine;

namespace EndlessRunner.Data
{
    [CreateAssetMenu(menuName = "Game Data/ Player Data", fileName = "Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Player Prefabs")]
        [SerializeField] private PlayerView player1Prefab;
        [SerializeField] private PlayerView player2Prefab;

        [Header("Player 1 Spawn")]
        [SerializeField] private Vector3 player1SpawnPosition;

        [Header("Player 2 Spawn (Voice/RL Player)")]
        [SerializeField] private Vector3 player2SpawnPosition;

        [Header("Jump Settings")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float groundDistance;
        [SerializeField] private float jumpTime;

        [Header("Other")]
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float gravityScale = 5f;

        public PlayerView Player1Prefab => player1Prefab;
        public PlayerView Player2Prefab => player2Prefab;

        public Vector3 Player1SpawnPosition => player1SpawnPosition;
        public Vector3 Player2SpawnPosition => player2SpawnPosition;

        public float JumpForce => jumpForce;
        public float GroundDistance => groundDistance;
        public float JumpTime => jumpTime;
        public LayerMask GroundLayer => groundLayer;
        public float GravityScale => gravityScale;
    }
}
