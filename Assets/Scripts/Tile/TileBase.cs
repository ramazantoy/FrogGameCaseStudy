
using UnityEngine;

namespace Tile
{
    public abstract class TileBase : MonoBehaviour
    {
        public Vector3Int TileCoordinate { get; set; }
        public abstract TileBase GetNeighbourByDirection();
        
    }
}
