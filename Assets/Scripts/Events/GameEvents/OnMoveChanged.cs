using Events.EventBusScripts;
using UnityEngine;

namespace Events.GameEvents
{
    public class OnMoveChanged : IEvent
    {
        public int MoveAmount;
    }
}
