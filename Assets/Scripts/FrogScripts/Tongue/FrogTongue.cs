using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace FrogScripts.Tongue
{
    public class FrogTongue : MonoBehaviour
    {
        [SerializeField]
        private Frog _myFrog;
        
        
        private LineRenderer _lineRenderer;
        
        
        private Vector3 _startPoint;
        private Vector3 _targetPoint;

        private TongueStateEnum _tongueStateEnum;
        
        private TongueState _currentState;
      
        
        public Vector3 TargetPoint => _targetPoint;
        
        private void Start()
        {
            _startPoint = _myFrog.transform.position + new Vector3(0, -.1f, -.2f);
            _lineRenderer = transform.GetComponent<LineRenderer>();
            
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0,_startPoint);



            _currentState = new TongueIdleState(this,_lineRenderer);
            _tongueStateEnum = TongueStateEnum.Idle;
            
            
            _currentState.OnEnter();
        }

        public void ClearRenderer()
        {
            if(_tongueStateEnum==TongueStateEnum.Idle) return;
            
            _lineRenderer.positionCount = 0;
            _tongueStateEnum = TongueStateEnum.Idle;
        }
        
    }
}
