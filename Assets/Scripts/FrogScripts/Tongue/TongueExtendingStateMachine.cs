using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace FrogScripts.Tongue
{
    public  class TongueExtendingStateMachine : TongueStateMachine
    {
        private readonly int _numOfMovementPoints = 50;
      
        public TongueExtendingStateMachine(FrogTongue tongue,LineRenderer lineRenderer) : base(tongue,lineRenderer)
        {
            
        }
        public override void OnEnter()
        { 
            ExtendTongue().Forget();
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            _tongue.OnExtendingStep();
        }
        
        private async UniTaskVoid ExtendTongue()
        {
            var obj = _tongue.GetPoint();
            
            await obj.transform.DOMove(_tongue.TargetPoint, .25f);
            OnExit();
        }

       
    }
}
