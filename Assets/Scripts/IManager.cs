using EndlessRunner.Event;
using UnityEngine;

namespace EndlessRunner.Common
{
    public interface IManager
    {
        void InitializeManager(IEventManager eventManager);
    }
}