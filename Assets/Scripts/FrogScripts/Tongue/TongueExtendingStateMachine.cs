using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Enums;

namespace FrogScripts.Tongue
{
    /// <summary>
    /// 
    /// </summary>
    public class TongueExtendingStateMachine : TongueStateMachine
    {
        /// <summary>
        /// Initializes the state machine with a reference to the frog's tongue.
        /// </summary>
        /// <param name="tongue"></param>
        public TongueExtendingStateMachine(FrogTongue tongue) : base(tongue)
        {
        }

        /// <summary>
        /// Called when entering the extending state. Initiates the tongue extension process by calling ExtendTongue().
        /// </summary>
        public override void OnEnter()
        {
            ExtendTongue().Forget();
        }

        public override void OnTick()
        {
        }

        /// <summary>
        /// Called when exiting the extending state. Triggers the next step in the tongue extension process by calling _tongue.OnExtendingStep().
        /// </summary>
        public override void OnExit()
        {
            _tongue.OnExtendingStep();
        }

        /// <summary>
        /// ExtendTongue(): An asynchronous method that handles the tongue extension process.
        /// - Retrieves a point for the tongue from _tongue.GetPoint().
        /// - Moves the point to the target position (_tongue.TargetPoint) using a tweening animation (DOMove).
        /// - Checks if the current hex view element's color matches the target color. If not, it calls _tongue.OnExtendingFail() and returns.
        /// - Scales up and down the hex view element.
        /// - Adds the current view to the list of visited views (_tongue.AddViewVisited()).
        /// - Checks if the current element type is a direction type. If it is, it changes the movement direction of the tongue.
        /// - Calls OnExit() to proceed to the next step or state.
        /// </summary>
        private async UniTaskVoid ExtendTongue()
        {
            var obj = _tongue.GetPoint();

            await obj.transform.DOMove(_tongue.TargetPoint, .25f);

            var currentElement = _tongue.GetCurrentHexView().HexViewElement;


            if (currentElement.ColorType != _tongue.TargetColorType)
            {
                _tongue.OnExtendingFail();
                return;
            }

            var hexView = _tongue.GetCurrentHexView();
            hexView.HexViewElement.ScaleUpDown();

            _tongue.AddViewVisited();


            if (currentElement.HexViewElementType == HexViewElementType.Direction)
            {
                _tongue.OnMovementDirectionChanged(currentElement.GetDirection());
            }

            OnExit();
        }
    }
}