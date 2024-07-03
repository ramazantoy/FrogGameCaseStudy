using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace FrogScripts.Tongue
{
    public class TongueRetractingStateMachine : TongueStateMachine
    {

        public TongueRetractingStateMachine(FrogTongue tongue,LineRenderer lineRenderer) : base(tongue,lineRenderer)
        {
        }

        public override void OnEnter()
        {
            RetractTongue().Forget();  
        }


        public override void OnTick()
        {
        }

        public override void OnExit()
        {
            _tongue.OnRetractingDone();
        }

        private async UniTaskVoid RetractTongue()
        {
            var usedPoints = _tongue.GetUsedPoints;
            if (usedPoints.Count < 2)
            {
                return;
            }
            
            for (var i = usedPoints.Count - 1; i > 0; i--)
            {
                var tasks = new List<UniTask>();
                for (int j = usedPoints.Count - 1; j >= i; j--)
                {
                    var targetPos = usedPoints[i - 1].transform.position;
                    tasks.Add(usedPoints[j].transform.DOMove(targetPos, 0.25f).ToUniTask());
                }
                
                await UniTask.WhenAll(tasks);

                await UniTask.Yield();
            }
            OnExit();
        }
        
    
    
    }
}