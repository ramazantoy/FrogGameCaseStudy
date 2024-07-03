using UnityEngine;
using System.Collections;

namespace FrogScripts.Tongue
{
    public class TongueRetractingStateMachine : TongueStateMachine
    {
        private float _targetLength = 0;
     
        public TongueRetractingStateMachine(FrogTongue tongue,LineRenderer lineRenderer) : base(tongue,lineRenderer)
        {
        }

        public override void OnEnter()
        {
            _tongue.StartCoroutine(RetractTongue());
        }


        public override void OnTick()
        {
        }

        public override void OnExit()
        {
        }

        private IEnumerator RetractTongue()
        {
          
            var startLength = GetLineLength();
            var duration = _lineRenderer.positionCount / 50 * 1.25f;
            var startTime = Time.time;

            while (Time.time - startTime < duration)
            {
                float t = (Time.time - startTime) / duration;
                SetLineLength(Mathf.Lerp(startLength, _targetLength, t));
                yield return null;
            }

            SetLineLength(_targetLength);
        }
        
        private float GetLineLength()
        {
            var length = 0f;
            for (int i = 1; i < _lineRenderer.positionCount; i++)
            {
                length += Vector3.Distance(_lineRenderer.GetPosition(i - 1), _lineRenderer.GetPosition(i));
            }
            return length;
        }
        
        private void SetLineLength(float length)
        {
            var currentLength = GetLineLength();
            var scale = length / currentLength;

            var positions = new Vector3[_lineRenderer.positionCount];
            positions[0] = _lineRenderer.GetPosition(0);
            for (int i = 1; i < _lineRenderer.positionCount; i++)
            {
                var direction = (_lineRenderer.GetPosition(i) - _lineRenderer.GetPosition(i - 1)).normalized;
                var distance = Vector3.Distance(_lineRenderer.GetPosition(i - 1), _lineRenderer.GetPosition(i)) * scale;
                positions[i] = positions[i - 1] + direction * distance;
            }

            _lineRenderer.positionCount = positions.Length;
            _lineRenderer.SetPositions(positions);
        }
    }
}