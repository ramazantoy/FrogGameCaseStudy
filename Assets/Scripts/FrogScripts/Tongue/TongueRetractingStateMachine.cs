using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;
using Events.EventBusScripts;
using Events.GameEvents;
using GrapeScripts;


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

        /// <summary>
        /// RetractTongue(): An asynchronous method that handles the tongue retracting process.
        /// - Retrieves the list of used points from _tongue.
        /// - If the movement was successful:
        ///   - Iterates through used points in reverse order.
        ///   - If the hex view is valid and not a direction type, attaches the hex view element to the corresponding point, resets its scale, and stops any ongoing tweens.
        /// - Creates a list of tasks for the points to move along their respective paths.
        /// - Awaits the completion of all movement tasks.
        /// - Calls OnExit() to proceed to the next step or state.
        /// </summary>
        private async UniTaskVoid RetractTongue()
        {
            var usedPoints = _tongue.GetUsedPoints;

            if (_tongue.IsMovementSuccessfullyCompleted)
            {
                for (var i = usedPoints.Count - 1; i >= 0; i--)
                {
                    var hexView = _tongue.GetLastVisitedView();

                    if (hexView == null || hexView.HexViewElement.HexViewElementType == HexViewElementType.Direction)
                        continue;

                    hexView.HexViewElement.transform.parent = usedPoints[i].transform;
                    hexView.HexViewElement.transform.DOKill();
                    hexView.HexViewElement.transform.localScale = Vector3.one * 4f;
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

                if (_tongue.IsMovementSuccessfullyCompleted)
                {
                    var grape = usedPoints[i].transform.GetComponentInChildren<Grape>();

                    if (grape != null)
                    {
                        grape.OnCollected(path.Length*.75f);
                    }
                }
             
                
                
                var tween = usedPoints[i].transform.DOPath(path, 0.25f * i);
                var task = tween.OnComplete(() =>
                {
                    if (_tongue.IsMovementSuccessfullyCompleted)
                    {
                        EventBus<OnCollectItemEvent>.Publish(new OnCollectItemEvent());
                    }
                  
                }).ToUniTask();
                
                tasks.Add(task);
            }

            await UniTask.WhenAll(tasks);

            OnExit();
        }
    }
}