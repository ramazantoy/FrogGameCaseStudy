
using UnityEngine;

namespace Tile
{
    public abstract class TileBase : MonoBehaviour
    {
        public Vector2Int TileCoordinate { get; set; }
        public abstract Vector2Int GetNeighbourByDirection(Direction direction);
        
    }
}
