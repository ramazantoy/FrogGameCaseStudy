using UnityEngine;

namespace FrogScripts.Tongue
{
    public abstract class TongueStateMachine 
    {
        protected readonly FrogTongue _tongue;
        protected readonly LineRenderer _lineRenderer;

        protected TongueStateMachine(FrogTongue tongue,LineRenderer lineRenderer)
        {
            _tongue = tongue;
            _lineRenderer = lineRenderer;
        }

        public abstract void OnEnter();
        public abstract void OnTick();
        public abstract void OnExit();
    }
}
