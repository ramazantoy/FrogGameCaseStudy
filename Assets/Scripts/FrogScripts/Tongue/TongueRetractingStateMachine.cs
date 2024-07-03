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

            for (int i = 2; i < usedPoints.Count; i++)
            {
                var tasks = new List<UniTask>();

                for (int j = i; j > 0; j--)
                {
                    var targetPos = usedPoints[j - 1].transform.position;
                    tasks.Add(usedPoints[j].transform.DOMove(targetPos, 0.25f).ToUniTask());
                }
                
                await UniTask.WhenAll(tasks);
                await UniTask.Yield();
            }
            OnExit();
        }
        
    
    
    }
}