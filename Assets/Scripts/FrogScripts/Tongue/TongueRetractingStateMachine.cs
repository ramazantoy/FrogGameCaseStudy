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

            for (var i = usedPoints.Count-1; i >=0; i--)
            {
                var hexView = _tongue.GetLastVisitedView();
                if (hexView != null && hexView.HexViewElement.HexViewElementType != HexViewElementType.Direction)
                {
                    hexView.HexViewElement.transform.parent = usedPoints[i].transform;
                    hexView.HexViewElement.transform.DOKill();
                    hexView.HexViewElement.transform.localScale = Vector3.one*4f;
                }
            }
            
            
            //i-1 i-2 i-3 i==0
            var tasks = new List<UniTask>();
            
            for (var i = 1; i < usedPoints.Count; i++)
            {
                var path = new Vector3[i + 1];
                for (var j = 0; j <= i; j++)
                {
                    path[j] = usedPoints[i - j].transform.position;
                }

                tasks.Add(usedPoints[i].transform.DOPath(path, 0.25f * i).ToUniTask());
            }

            await UniTask.WhenAll(tasks);

            OnExit();
        }
        
    
    
    }
}