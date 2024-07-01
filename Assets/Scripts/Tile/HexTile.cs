using UnityEngine;

namespace Tile
{
    public class HexTile : TileBase
    {
        /// <summary>
        /// Returns the neighboring coordinate of a hex tile based on the given direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override Vector2Int GetNeighbourByDirection(Direction direction)
        {
            return TileCoordinate.GetCoordinate(direction);
        }
    }
}
