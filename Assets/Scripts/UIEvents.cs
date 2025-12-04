using UnityEngine;
using System;

namespace EndlessRunner.Event
{
    public class UIEvents
    {
        public IEventController<Action> OnStartButtonClicked { get; }
        public IEventController<Action> OnRestartButtonClicked { get; }

        public UIEvents()
        {
            OnStartButtonClicked = new DeferredEventController<Action>();
            OnRestartButtonClicked = new DeferredEventController<Action>();
        }
    }
}