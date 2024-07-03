using Enums;
using Interfaces;
using UnityEngine;

namespace HexViewScripts
{
    public abstract class HexViewElement : MonoBehaviour,IHexViewElement
    {
        public abstract void SetDirection(Direction direction);
        public HexView MyHexView { get; set; }
        public abstract void OnAction();

        public abstract void SetColor(ColorType colorType);
    }
}
