using UnityEngine;


namespace EndlessRunner.Event
{
    public class EventManager: IEventManager
    {
        public GameEvents GameEvents { get;}
        public UIEvents UIEvents { get;}
        public ObstacleEvents ObstacleEvents { get;}
        public PlayerEvents PlayerEvents { get;}

        public EventManager()
        {
            GameEvents = new GameEvents();
            UIEvents = new UIEvents();
            ObstacleEvents = new ObstacleEvents();
            PlayerEvents = new PlayerEvents();
        }
    }
}