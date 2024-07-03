using UnityEngine;

namespace FrogScripts.Tongue
{
    public class TongueIdleStateMachine : TongueStateMachine
    {
        public TongueIdleStateMachine(FrogTongue tongue,LineRenderer lineRenderer) : base(tongue,lineRenderer)
        {
            
        }

        public override void OnEnter()
        { 
            _lineRenderer.positionCount = 1;
        }

        public override void OnTick()
        {
           
     
        }

        public override void OnExit()
        {
        
        }
    }
}
