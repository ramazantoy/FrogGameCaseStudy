using System;
using System.Collections.Generic;
using Enums;
using Extensions;
using Tile;
using UnityEngine;

namespace FrogScripts.Tongue
{
    public class FrogTongue : MonoBehaviour
    { 
        private LineRenderer _lineRenderer;

        [SerializeField]
        private GameObject _tonguePointPref;

        private List<GameObject> _availablePoints = new List<GameObject>();
        [SerializeField]
        private List<GameObject> _usedPoints;
        
        
        
        public Vector3 TargetPoint { get; set; }

        private void FixedUpdate()
        {
            _lineRenderer.positionCount = _usedPoints.Count;
            
            for (var i = 0; i < _usedPoints.Count; i++)
            {
                _lineRenderer.SetPosition(i, _usedPoints[i].transform.position);
            }
        }

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
        
        

        private void Init()
        {
            _tongueIdleStateMachine = new TongueIdleStateMachine(this, _lineRenderer);
            _tongueExtendingStateMachine = new TongueExtendingStateMachine(this, _lineRenderer);
            _tongueRetractingStateMachine = new TongueRetractingStateMachine(this, _lineRenderer);
            
        }
        
        private void Start()
        {
            _lineRenderer = transform.GetComponent<LineRenderer>();

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
            
            if (targetTile == null)
            {
              SetTongueState(TongueState.Idle);
            }
            var targetTilePos = targetTile.transform.position;
            TargetPoint = new Vector3(targetTilePos.x, targetTilePos.y, -.25f);
            SetTongueState(TongueState.Extending);
        }

        public void OnExtendingStep()
        {
            
            _targetCoordinate = _targetCoordinate.GetCoordinate(_movementDirection);
            
            var targetTile = GetTargetTile();
            
            if (targetTile == null)
            {
                SetTongueState(TongueState.Retracting);
                return;
            }

            var targetTilePos = targetTile.transform.position;
            TargetPoint = new Vector3(targetTilePos.x, targetTilePos.y, -.25f);
            
            _currentStateMachine.OnEnter();
        }

        private HexTile GetTargetTile()
        {
           return GameFuncs.GetTile(_targetCoordinate);
        }

        public GameObject GetPoint()
        {
            if (_availablePoints.Count > 0)
            {
                var point = _availablePoints[0];
                point.transform.position = GetLastPoint;
                _availablePoints.RemoveAt(0);
                _usedPoints.Add(point);
                return point;
            }

            var newPoint = Instantiate(_tonguePointPref, transform);
            newPoint.transform.position = GetLastPoint;
            _usedPoints.Add(newPoint);
            return newPoint;
        }

        private Vector3 GetLastPoint => _usedPoints[^1].gameObject.transform.position;

        public List<GameObject> GetUsedPoints => _usedPoints;

        public void OnRetractingDone()
        {
         
            gameObject.SetActive(false);
            
        }




    }
}
