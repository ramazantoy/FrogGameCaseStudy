using Enums;
using HexViewScripts;
using Interfaces;
using UnityEngine;

namespace GrapeScripts
{
    public class Grape : HexViewElement,IHexViewElement
    {
        [SerializeField]
        private GrapeData _properties;

        public override void SetDirection(Direction direction)
        {
            
        }

        public override void OnAction()
        {
         
        }

        public override void SetColor(ColorType colorType)
        {
         _properties.SetGrapeColor(colorType);
        }
    }
}
