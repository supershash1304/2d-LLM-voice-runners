using System;
using UnityEngine;

namespace EndlessRunner.Event
{
    public class ObstacleEvents
    {
        public IEventController<Action<int>> OnObstacleAvoided {  get; }

        public ObstacleEvents()
        {
            OnObstacleAvoided = new DeferredEventController<Action<int>>();
        }
    }
}