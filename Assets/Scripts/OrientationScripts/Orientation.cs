using Enums;
using HexViewScripts;
using Interfaces;
using UnityEngine;

namespace OrientationScripts
{
    public class Orientation : HexViewElement,IHexViewElement
    {
        [SerializeField]
        private OrientationData _properties;

        public override void SetDirection(Direction direction)
        {
            
        }

        public override void OnAction()
        {
            throw new System.NotImplementedException();
        }

        public override void SetColor(ColorType colorType)
        {
            _properties.SetOrientationColor(colorType);
        }
    }
}
