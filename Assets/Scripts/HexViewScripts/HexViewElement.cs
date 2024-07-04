using Enums;
using Interfaces;
using UnityEngine;

namespace HexViewScripts
{
    public abstract class HexViewElement : MonoBehaviour,IHexViewElement
    {

        public HexViewElementType HexViewElementType { get; set; }
        public ColorType ColorType { get; set; }
        public Vector2Int Coordinate { get; set; }
        public abstract void SetDirection(Direction direction);

        public abstract Direction GetDirection();
        public HexView MyHexView { get; set; }
        public abstract void OnAction();

        public abstract void SetColor(ColorType colorType);
    }
}
