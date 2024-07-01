using System;
using Enums;
using UnityEngine;

namespace Tile
{
    public static class TileExtensions 
    {
        /// <summary>
        /// Returns the neighboring coordinate of a hex tile based on the given direction.
        /// </summary>
        /// <param name="coordinate">The current coordinate of the hex tile.</param>
        /// <param name="direction">The direction to find the neighboring coordinate.</param>
        /// <returns>The neighboring coordinate in the specified direction.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when an invalid direction is provided.</exception>
        public static Vector2Int GetCoordinate(this Vector2Int coordinate, Direction direction)
        {
            if (coordinate.x % 2 == 0)
            {
                return direction switch
                {
                    Direction.Up => coordinate + new Vector2Int(0, -1),
                    Direction.Down => coordinate + new Vector2Int(0, 1),
                    Direction.UpRight => coordinate + new Vector2Int(1, -1),
                    Direction.UpLeft => coordinate + new Vector2Int(-1, -1),
                    Direction.DownRight => coordinate + new Vector2Int(1, 0),
                    Direction.DownLeft => coordinate + new Vector2Int(-1, 0),
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                };
            }
            return direction switch
            {
                Direction.Up => coordinate + new Vector2Int(0, -1),
                Direction.Down => coordinate + new Vector2Int(0, 1),
                Direction.UpRight => coordinate + new Vector2Int(1, 0),
                Direction.UpLeft => coordinate + new Vector2Int(-1, 0),
                Direction.DownRight => coordinate + new Vector2Int(1, 1),
                Direction.DownLeft => coordinate + new Vector2Int(-1, 1),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            
        }
    }
}
