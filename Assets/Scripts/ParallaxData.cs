using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunner.Parallax
{
    [CreateAssetMenu(menuName = "Game Data/ Parallax Data", fileName = "Parallax Data")]
    public class ParallaxData : ScriptableObject
    {
        [SerializeField] List<ParallaxLayer> parallaxLayers;

        public List<ParallaxLayer> ParallaxLayers => parallaxLayers;
    }
}