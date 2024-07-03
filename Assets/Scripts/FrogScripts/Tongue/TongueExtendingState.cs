using UnityEngine;
using System.Collections;

namespace FrogScripts.Tongue
{
    public  class TongueExtendingState : TongueState
    {
        private readonly int _numOfMovementPoints = 50;
      
        public TongueExtendingState(FrogTongue tongue,LineRenderer lineRenderer) : base(tongue,lineRenderer)
        {
            
        }
        public override void OnEnter()
        {
            _tongue.StartCoroutine(ExtendTongue());
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            
        }
        
        private IEnumerator ExtendTongue()
        {
            
            var currentPositionCount = _lineRenderer.positionCount;
            var targetPoint = _tongue.TargetPoint;
            var startPoint = _lineRenderer.GetPosition(currentPositionCount - 1);
            
            var startingIndex = currentPositionCount;

            for (var i = 1; i <= _numOfMovementPoints; i++)
            {
                var t = i / (float)(_numOfMovementPoints + 1); 

                var horizontalPosition = Vector3.Lerp(startPoint, targetPoint, t);
                var position = new Vector3(horizontalPosition.x, horizontalPosition.y, horizontalPosition.z);
                _lineRenderer.positionCount++; 
                _lineRenderer.SetPosition(startingIndex + i-1, position);
                yield return new WaitForSeconds(0.01f);
            }
        }


       
    }
}
