using System;
using Enums;
using Extensions;
using Tile;
using UnityEngine;

namespace FrogScripts.Tongue
{
    public class FrogTongue : MonoBehaviour
    { 
        private LineRenderer _lineRenderer;
        private Vector3 _startPoint;

      
        
        [SerializeField]
        private TongueState _tongueState;


        public TongueState TongueState => _tongueState;
        
        private TongueStateMachine _currentStateMachine;
        private TongueIdleStateMachine _tongueIdleStateMachine;
        private TongueExtendingStateMachine _tongueExtendingStateMachine;
        private TongueRetractingStateMachine _tongueRetractingStateMachine;

        #region MovementValues

        private ColorType _targetColorType;
        private Vector2Int _targetCoordinate;
        private Direction _movementDirection;

        #endregion

        public Vector3 TargetPoint { get; set; }
        

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
            
            Init();
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

        public void StartExtending(Vector2Int currentCoordinate, Direction direction,ColorType targetColorType)
        {
            _targetColorType = targetColorType;
            _movementDirection = direction;
            
            _targetCoordinate = currentCoordinate.GetCoordinate(direction);
            

            var targetTile = GetTargetTile();
            Debug.LogError(_targetCoordinate);
            Debug.LogError("Target Tile " +targetTile);
            if (targetTile == null)
            {
                SetTongueState(TongueState.Idle); 
                return;
            }
            TargetPoint = targetTile.transform.position;
            SetTongueState(TongueState.Extending);
        }

        public void OnExtendingStepDone()
        {
            
            _targetCoordinate = _targetCoordinate.GetCoordinate(_movementDirection);
            
            var targetTile = GetTargetTile();
            
            Debug.Log(targetTile);
            
            if (targetTile == null)
            {
                SetTongueState(TongueState.Retracting);
                return;
            }
            TargetPoint = targetTile.transform.position;
            _currentStateMachine.OnEnter();
        }

        private HexTile GetTargetTile()
        {
           return GameFuncs.GetTile(_targetCoordinate);
        }

        
        
    }
}
