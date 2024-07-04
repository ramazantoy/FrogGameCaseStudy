using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;

namespace FrogScripts.Tongue
{
    public class TongueRetractingStateMachine : TongueStateMachine
    {

        public TongueRetractingStateMachine(FrogTongue tongue) : base(tongue)
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

            for (int i = usedPoints.Count - 1; i > 0; i--)
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