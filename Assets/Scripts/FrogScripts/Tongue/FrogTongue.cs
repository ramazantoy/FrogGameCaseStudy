using System;
using System.Collections.Generic;
using Enums;
using Extensions;
using HexViewScripts;
using Tile;
using UnityEngine;

namespace FrogScripts.Tongue
{
    /// <summary>
    /// The FrogTongue class manages the behavior and state of the frog's tongue in the game.
    /// It handles tongue extension and retraction, interacts with hex tiles and views, 
    /// and manages the tongue's movement and state transitions.
    /// 
    /// Main Components:
    /// - Private fields and properties for managing tongue points, state, and movement.
    /// - Initialization and state transition methods for setting up and switching between different states.
    /// - Methods for handling tongue extension and retraction, including movement, scaling, and interactions with hex elements.
    /// - Helper methods for managing visited tiles and views, and for resetting and retrieving tongue points.
    /// 
    /// Usage:
    /// - Use StartExtending() to initiate the tongue's extending process.
    /// - The tongue's state is managed through the SetTongueState() method and the various state machine classes.
    /// - The FixedUpdate() method updates the tongue's line renderer positions in each frame.
    /// </summary>
    public class FrogTongue : MonoBehaviour
    {
        private Frog _myFrog;

        public Frog Frog
        {
            set { _myFrog = value; }
        }

        private LineRenderer _lineRenderer;

        [SerializeField] private GameObject _tonguePointPref;

        private List<GameObject> _availablePoints = new List<GameObject>();

        [SerializeField] private List<GameObject> _usedPoints;

        private List<HexView> _visitedHexViews = new List<HexView>();
        private List<HexTile> _visitedTiles = new List<HexTile>();


        /// <summary>
        /// Adds a hex view to the visited list if not already added.
        /// </summary>
        public void AddViewVisited()
        {
            var hexTile = GetTargetTile();

            if (hexTile == null) return;

            if (_visitedTiles.Contains(hexTile)) return;
            _visitedTiles.Add(hexTile);

            var hexView = hexTile.GetTopStackElement();
            if (_visitedHexViews.Contains(hexView)) return;
            _visitedHexViews.Add(hexView);
        }

        /// <summary>
        /// Retrieves the last visited hex view.
        /// </summary>
        /// <returns></returns>
        public HexView GetLastVisitedView()
        {
            if (_visitedHexViews.Count <= 0) return null;

            var view = _visitedHexViews[^1];

            _visitedHexViews.RemoveAt(_visitedHexViews.Count - 1);
            return view;
        }


        public Vector3 TargetPoint { get; set; }

        /// <summary>
        /// Updated line renderer every frame
        /// </summary>
        private void FixedUpdate()
        {
            _lineRenderer.positionCount = _usedPoints.Count;

            for (var i = 0; i < _usedPoints.Count; i++)
            {
                _lineRenderer.SetPosition(i, _usedPoints[i].transform.position);
            }
        }

        [SerializeField] private TongueState _tongueState;

        public TongueState TongueState => _tongueState;

        private TongueStateMachine _currentStateMachine;
        private TongueIdleStateMachine _tongueIdleStateMachine;
        private TongueExtendingStateMachine _tongueExtendingStateMachine;
        private TongueRetractingStateMachine _tongueRetractingStateMachine;

        #region MovementValues

        public ColorType TargetColorType { private set; get; }
        private Vector2Int _targetCoordinate;
        private Direction _movementDirection;

        public bool IsMovementSuccessfullyCompleted { private set; get; }

        #endregion


        /// <summary>
        /// Initializes state machines.
        /// </summary>
        private void Init()
        {
            _tongueIdleStateMachine = new TongueIdleStateMachine(this);
            _tongueExtendingStateMachine = new TongueExtendingStateMachine(this);
            _tongueRetractingStateMachine = new TongueRetractingStateMachine(this);
        }

        /// <summary>
        ///  Initializes components and sets the tongue state to idle.
        /// </summary>
        private void Start()
        {
            _lineRenderer = transform.GetComponent<LineRenderer>();

            Init();
            SetTongueState(TongueState.Idle);
        }

        /// <summary>
        /// Changes the tongue state and handles the state transition.
        /// </summary>
        /// <param name="tongueState"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void SetTongueState(TongueState tongueState)
        {
            if (tongueState == _tongueState) return;

            _tongueState = tongueState;

            switch (tongueState)
            {
                case TongueState.Idle:
                    _currentStateMachine = _tongueIdleStateMachine;
                    _currentStateMachine.OnEnter();
                    break;
                case TongueState.Retracting:
                    _myFrog.OnRetracting();
                    _currentStateMachine = _tongueRetractingStateMachine;
                    _currentStateMachine.OnEnter();
                    break;
                case TongueState.Extending:
                    _currentStateMachine = _tongueExtendingStateMachine;
                    _currentStateMachine.OnEnter();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(tongueState), tongueState, null);
            }
        }

        /// <summary>
        /// Starts the tongue extending process.
        /// </summary>
        /// <param name="currentCoordinate"></param>
        /// <param name="direction"></param>
        /// <param name="targetColorType"></param>
        public void StartExtending(Vector2Int currentCoordinate, Direction direction, ColorType targetColorType)
        {
            TargetColorType = targetColorType;
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

        /// <summary>
        /// Gets the current hex view from the target tile.
        /// </summary>
        /// <returns></returns>
        public HexView GetCurrentHexView()
        {
            var tile = GetTargetTile();

            return tile == null ? null : tile.GetTopStackElement();
        }

        /// <summary>
        /// Handles the steps during tongue extension.
        /// </summary>
        public void OnExtendingStep()
        {
            _targetCoordinate = _targetCoordinate.GetCoordinate(_movementDirection);

            var targetTile = GetTargetTile();

            if (targetTile == null)
            {
                IsMovementSuccessfullyCompleted = true;
                SetTongueState(TongueState.Retracting);
                return;
            }

            var targetTilePos = targetTile.transform.position;
            TargetPoint = new Vector3(targetTilePos.x, targetTilePos.y, -.25f);

            _currentStateMachine.OnEnter();
        }

        /// <summary>
        /// Retrieves the target hex tile.
        /// </summary>
        /// <returns></returns>
        private HexTile GetTargetTile()
        {
            return GameFuncs.GetTile(_targetCoordinate);
        }

        /// <summary>
        /// Retrieves or creates a tongue point.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the position of the last tongue point.
        /// </summary>
        private Vector3 GetLastPoint => _usedPoints[^1].gameObject.transform.position;

        /// <summary>
        /// Gets the list of used points.
        /// </summary>
        public List<GameObject> GetUsedPoints => _usedPoints;

        /// <summary>
        /// Handles actions after the tongue retracting is done.
        /// </summary>
        public void OnRetractingDone()
        {
            if (IsMovementSuccessfullyCompleted)
            {
                BlowYourSelf();
            }

            ResetPoints();
            SetTongueState(TongueState.Idle);
        }

        /// <summary>
        /// Blows the top element of each visited tile and notifies the frog of movement completion.
        /// </summary>
        private void BlowYourSelf()
        {
            foreach (var hexTile in _visitedTiles)
            {
                hexTile.BlowTopElement().Forget();
            }

            _myFrog.OnMovementDone();
        }

        /// <summary>
        /// Resets the tongue points for reuse.
        /// </summary>
        private void ResetPoints()
        {
            var point = _usedPoints[0];
            _usedPoints.RemoveAt(0);

            foreach (var usedPoint in _usedPoints)
            {
                _availablePoints.Add(usedPoint);
            }

            _usedPoints.Clear();
            _usedPoints.Add(point);
        }

        /// <summary>
        /// Handles failure during tongue extension.
        /// </summary>
        public void OnExtendingFail()
        {
            IsMovementSuccessfullyCompleted = false;
            SetTongueState(TongueState.Retracting);
        }

        /// <summary>
        /// Changes the movement direction.
        /// </summary>
        /// <param name="newDirection"></param>
        public void OnMovementDirectionChanged(Direction newDirection)
        {
            _movementDirection = newDirection;
        }
    }
}