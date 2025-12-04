using UnityEngine;

namespace EndlessRunner.Parallax
{
    public class ParallaxEffect : MonoBehaviour
    {
        private float moveSpeed;
        private float offset;

        private Vector2 startPosition;

        private bool isParallaxActive = false;

        public void Initialize(float moveSpeed, float offset)
        {
            this.moveSpeed = moveSpeed;
            this.offset = offset;
            this.startPosition = transform.position;
        }

        private void Update()
        {
            if (!isParallaxActive) return;

            float newXPosition = Mathf.Repeat(Time.time * -moveSpeed, offset);
            transform.position = startPosition + Vector2.right * newXPosition;
        }

        public void StartParallax() => isParallaxActive = true;

        public void StopParallax() => isParallaxActive = false;
    }
}