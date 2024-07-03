using UnityEngine;

namespace FrogScripts.Tongue
{
    public class TongueIdleState : TongueState
    {
        public TongueIdleState(FrogTongue tongue,LineRenderer lineRenderer) : base(tongue,lineRenderer)
        {
            
        }

        public override void OnEnter()
        { 
            _tongue.ClearRenderer();
        }

        public override void OnTick()
        {
           
     
        }

        public override void OnExit()
        {
        
        }
    }
}
