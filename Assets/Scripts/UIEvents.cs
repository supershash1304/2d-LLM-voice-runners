using UnityEngine;
using System;

namespace EndlessRunner.Event
{
    public class UIEvents
    {
        public IEventController<Action> OnStartButtonClicked { get; }
        public IEventController<Action> OnRestartButtonClicked { get; }
        public IEventController<Action> OnQuitButtonClicked { get; }   // NEW EVENT

        public UIEvents()
        {
            OnStartButtonClicked = new DeferredEventController<Action>();
            OnRestartButtonClicked = new DeferredEventController<Action>();
            OnQuitButtonClicked = new DeferredEventController<Action>(); // NEW EVENT
        }
    }
}
