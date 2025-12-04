using UnityEngine;

namespace EndlessRunner.Parallax
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public ParallaxLayerType parallaxLayerType;
        public ParallaxEffect parallaxEffectPrefab;
        public float moveSpeed;
        public float offset;
    }
}