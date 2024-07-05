using Events.EventBusScripts;
using FrogScripts;

namespace Events.GameEvents
{
    public class OnFrogMovementDoneEvent : IEvent
    {
        public Frog Frog;
    }
}
