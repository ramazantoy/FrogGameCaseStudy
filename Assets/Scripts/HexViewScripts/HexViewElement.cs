using Enums;
using UnityEngine;

namespace HexViewScripts
{
    public abstract class HexViewElement : MonoBehaviour
    {

        public HexViewElementType HexViewElementType { get; set; }
        public ColorType ColorType { get; set; }
        public Vector2Int Coordinate { get; set; }
        public abstract void SetDirection(Direction direction);

        public abstract Direction GetDirection();
        public HexView MyHexView { get; set; }
        public abstract void ScaleUpDown();

        public abstract void SetColor(ColorType colorType);

        public abstract void OnCollected(float time);
    }
}
