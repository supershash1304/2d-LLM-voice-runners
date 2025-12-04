using EndlessRunner.Common;
using EndlessRunner.Event;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunner.Parallax
{
    public class ParallaxManager : MonoBehaviour, IManager
    {
        [SerializeField] private ParallaxData parallaxData;

        private IEventManager eventManager;
        private List<ParallaxEffect> activeParallaxEffects = new List<ParallaxEffect>();

        public void InitializeManager(IEventManager eventManager)
        {
            SetManagerDependencies(eventManager);
            RegisterEventListeners();
            SpawnParallaxLayers();
        }

        private void SetManagerDependencies(IEventManager eventManager) => this.eventManager = eventManager;

        private void RegisterEventListeners() => eventManager.GameEvents.OnGameStateUpdated.AddListener(OnGameStateUpdated);

        private void SpawnParallaxLayers()
        {
            foreach (ParallaxLayer parallaxLayer in parallaxData.ParallaxLayers)
            {
                if (parallaxLayer.parallaxEffectPrefab == null)
                {
                    Debug.LogWarning("Parallax layer doesnot exist");
                    continue;
                }

                ParallaxEffect parallaxEffect = GameObject.Instantiate<ParallaxEffect>(parallaxLayer.parallaxEffectPrefab, this.transform);
                parallaxEffect.Initialize(parallaxLayer.moveSpeed, parallaxLayer.offset);
                activeParallaxEffects.Add(parallaxEffect);
            }
        }

        private void OnGameStateUpdated(GameState state)
        {
            if (state == GameState.IN_GAME) foreach (ParallaxEffect parallaxEffect in activeParallaxEffects) parallaxEffect.StartParallax();
            else foreach (ParallaxEffect parallaxEffect in activeParallaxEffects) parallaxEffect.StopParallax();
        }
    }
}