using System;
using DG.Tweening;
using Enums;
using HexViewScripts;
using UnityEngine;

namespace GrapeScripts
{
    public class Grape : HexViewElement
    {
        [SerializeField]
        private GrapeData _properties;

        public override void SetDirection(Direction direction)
        {
            
        }

        public override Direction GetDirection()
        {
            return Direction.None;
        }

        public override void ScaleUpDown()
        {
            var scale = transform.lossyScale;
            transform.DOScale(scale * 1.25f, .15f).OnComplete((() =>
            {
                transform.DOScale(Vector3.one, .15f);
            }));
        }

        public override void OnCollected(float time)
        {
            transform.DOScale(Vector3.zero, time);

        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        public override void SetColor(ColorType colorType)
        {
         _properties.SetGrapeColor(colorType);
        }
    }
}
