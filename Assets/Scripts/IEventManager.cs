using UnityEngine;

namespace EndlessRunner.Event
{
    public interface IEventManager
    {
        GameEvents GameEvents { get; }
        UIEvents UIEvents { get; }
        ObstacleEvents ObstacleEvents { get; }
        PlayerEvents PlayerEvents { get; }
    }
}