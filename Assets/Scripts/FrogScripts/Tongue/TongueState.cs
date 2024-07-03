using UnityEngine;

namespace FrogScripts.Tongue
{
    public abstract class TongueState 
    {
        protected readonly FrogTongue _tongue;
        protected readonly LineRenderer _lineRenderer;

        protected TongueState(FrogTongue tongue,LineRenderer _lineRenderer)
        {
            _tongue = tongue;
        }

        public abstract void OnEnter();
        public abstract void OnTick();
        public abstract void OnExit();
    }
}
