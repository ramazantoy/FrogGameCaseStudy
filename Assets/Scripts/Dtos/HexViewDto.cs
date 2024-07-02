
using Enums;

namespace Dtos
{
    
    [System.Serializable]
    public class HexViewDto
    {
        public Direction Direction=Direction.None;
        public HexViewElementType HexViewElementType;
        public ColorType HexViewColorType;

    }
}
