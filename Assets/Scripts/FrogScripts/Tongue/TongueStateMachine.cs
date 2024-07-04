using Enums;
using UnityEngine;

namespace FrogScripts.Tongue
{
    public abstract class TongueStateMachine 
    {
        protected readonly FrogTongue _tongue;

        protected TongueStateMachine(FrogTongue tongue)
        {
            _tongue = tongue;
        }

        public abstract void OnEnter();
        public abstract void OnTick();
        public abstract void OnExit();
    }
}
