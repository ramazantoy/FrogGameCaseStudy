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

        private Direction _orientationDirection;

        public override void SetDirection(Direction direction)
        {
            _orientationDirection = direction;
            transform.localRotation=Quaternion.Euler(new Vector3(0,0,GetZDirection(direction)));
        }

        public override Direction GetDirection()
        {
            return _orientationDirection;
        }

        private float GetZDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Down or Direction.None => 0,
                Direction.Up => 180,
                Direction.DownLeft => -53f,
                Direction.DownRight => 53f,
                Direction.UpLeft => -110f,
                Direction.UpRight => 110f,
                _ => 0f
            };
        }

        public override void OnAction()
        {
      
        }

        public override void SetColor(ColorType colorType)
        {
            _properties.SetOrientationColor(colorType);
        }
    }
}
