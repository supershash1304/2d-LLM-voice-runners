using System;
using UnityEngine;

namespace EndlessRunner.Event
{
    public class PlayerEvents
    {
        public IEventController<Action<int>> OnScoreUpdated { get; }
        public IEventController<Action> OnHitByObstacle { get; }
        public IEventController<Action<int, int>> OnGameover {  get; }

        public PlayerEvents()
        {
            OnScoreUpdated = new DeferredEventController<Action<int>>();
            OnHitByObstacle = new DeferredEventController<Action>();
            OnGameover = new DeferredEventController<Action<int, int>>();
        }
    }
}