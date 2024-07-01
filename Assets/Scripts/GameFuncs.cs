
using System;
using Tile;
using UnityEngine;

/// <summary>
/// Static functions repository for game operations related to tiles.
/// </summary>
public class GameFuncs
{
    /// <summary>
    /// Retrieves a HexTile at specified coordinates from the game grid.
    /// </summary>
    public static Func<Vector2Int, HexTile> GetTile;
}
