using System.Collections.Generic;
using Dtos;
using Tile;
using UnityEngine;
using UnityEngine.Serialization;

namespace GridSystem
{
    /// <summary>
    /// ScriptableObject container for storing grid building data
    /// Offset value in the X direction for grid building.
    /// Offset value in the Y direction for grid building.
    /// Prefab of the hex tile used for grid building.
    /// List of base settings for different levels.
    /// </summary>
    [CreateAssetMenu(fileName = "GridBuilderDataContainer", menuName = "ScriptableObjects/GridBuilderDataContainer")]
    public class GridBuilderDataContainer : ScriptableObject
    {
        public float XOffset;
        public float YOffset;
        public HexTile TilePref;
        public List<LevelBaseSettings> LevelBaseSettings;
    }

    /// <summary>
    /// Serializable struct containing base settings for a level.
    /// Width of the grid for the level.
    /// Height of the grid for the level.
    /// Position of the camera for the level.
    /// Size of the camera for the level.
    /// </summary>
    [System.Serializable]
    public struct LevelBaseSettings
    {
        public int GridWidth;
        public int GridHeight;
        public Vector3 CameraPos;
        public float CameraSize;

        public List<LevelTilesView> LevelTilesViews;
    }
    
    /// <summary>
    /// Level data class for converting to dictionary
    /// </summary>

    [System.Serializable]
    public struct LevelTilesView
    {
        public Vector2Int Key;
        public List<HexViewDto> Value;
    }
}