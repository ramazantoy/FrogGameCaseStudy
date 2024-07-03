using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace FrogScripts.Tongue
{
    public class FrogTongue : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        
        private Vector3 _startPoint;
        
        private TongueState _tongueState;
        
        private TongueStateMachine _currentStateMachine;
        private TongueIdleStateMachine _tongueIdleStateMachine;
        private TongueExtendingStateMachine _tongueExtendingStateMachine;
        private TongueRetractingStateMachine _tongueRetractingStateMachine;


        public Vector3 TargetPoint
        {
            get
            {
                return Vector3.zero;
            }
        }

        private void Init()
        {
            _tongueIdleStateMachine = new TongueIdleStateMachine(this, _lineRenderer);
            _tongueExtendingStateMachine = new TongueExtendingStateMachine(this, _lineRenderer);
            _tongueRetractingStateMachine = new TongueRetractingStateMachine(this, _lineRenderer);
            
        }
        
        private void Start()
        {
            _startPoint = transform.position + new Vector3(0, -.1f, -.2f);
            _lineRenderer = transform.GetComponent<LineRenderer>();
            
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0,_startPoint);
            
            SetTongueState(TongueState.Idle);
        }

        private void SetTongueState(TongueState tongueState)
        {
            if(tongueState==_tongueState) return;

            _tongueState = tongueState;
            
            switch (tongueState)
            {
                case TongueState.Idle :
                    _currentStateMachine = _tongueIdleStateMachine;
                    _currentStateMachine.OnEnter();
                    break;
                case TongueState.Retracting :
                    _currentStateMachine = _tongueRetractingStateMachine;
                    _currentStateMachine.OnEnter();
                    break;
                case TongueState.Extending :
                    _currentStateMachine = _tongueExtendingStateMachine;
                    _currentStateMachine.OnEnter();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(tongueState), tongueState, null);
            }
        }
        
        
    }
}
