using EndlessRunner.Common;
using System;
using UnityEngine;

namespace EndlessRunner.Event
{
    public class GameEvents
    {
        public IEventController<Action<GameState>> OnGameStateUpdated { get; }

        public GameEvents() 
        {
            OnGameStateUpdated = new DeferredEventController<Action<GameState>>();
        }
    }
}