using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;

namespace FrogScripts.Tongue
{
    public  class TongueExtendingStateMachine : TongueStateMachine
    {
        public TongueExtendingStateMachine(FrogTongue tongue) : base(tongue)
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

            var currentElement = _tongue.GetCurrentHexViewElement();
            
            
            if (currentElement.ColorType != _tongue.TargetColorType)
            {
                _tongue.OnExtendingFail();
                return;
            } 
            if (currentElement.HexViewElementType == HexViewElementType.Direction)
            {
                _tongue.OnMovementDirectionChanged(currentElement.GetDirection());
            }
            
            OnExit();
        }

       
    }
}
