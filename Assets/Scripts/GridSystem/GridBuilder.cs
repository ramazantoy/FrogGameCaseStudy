using System;
using System.Collections.Generic;
using System.Linq;
using Dtos;
using Tile;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using TileBase = Tile.TileBase;

namespace GridSystem
{
    public class GridBuilder : MonoBehaviour
    {
        [SerializeField] private GridBuilderDataContainer _gridBuilderDataContainer;

        private HexTile[,] _tiles;


#if UNITY_EDITOR

        public List<HexTile> TileList;
        public int GridWidth;
        public int GridHeight;
#endif

        private void Start()
        {
            BuildGrid(false);
        }

        private void OnEnable()
        {
            GameFuncs.GetTile += GetTile;
        }

        private void OnDisable()
        {
            GameFuncs.GetTile -= GetTile;
        }

        /// <summary>
        /// Builds a grid of tiles.
        /// </summary>
        /// <param name="onEditor">Specifies whether the grid is being built in the editor (true by default).</param>
        public void BuildGrid(bool onEditor = true)
        {
            RemoveTiles(onEditor);


            var xOffset = _gridBuilderDataContainer.XOffset;
            var yOffset = _gridBuilderDataContainer.YOffset;
            var tilePref = _gridBuilderDataContainer.TilePref;

            var levelDictionary = _gridBuilderDataContainer.LevelBaseSettings[0].LevelTilesViews
                .ToDictionary(item => item.Key, item => item.Value);

#if UNITY_EDITOR
            var gridWidth = GridHeight;
            var gridHeight = GridHeight;

#else
               var  gridHeight = _gridBuilderDataContainer.LevelBaseSettings[0].GridWidth;
               var gridWidth = _gridBuilderDataContainer.LevelBaseSettings[0].GridHeight;
            
#endif
            _tiles = new HexTile[gridWidth, gridHeight];


            for (var x = 0; x < gridWidth; x++)
            {
                var xPos = xOffset * x;


                for (var y = 0; y < gridHeight; y++)
                {
                    var yPos = yOffset * y - (x % 2 == 1 ? 0 : yOffset / 2);
#if UNITY_EDITOR

                    var tileTemp = onEditor
                        ? PrefabUtility.InstantiatePrefab(tilePref, transform) as HexTile
                        : Instantiate(tilePref, transform);
#else
                   var tileTemp = Instantiate(tilePref, transform);
#endif


                    if (tileTemp == null) continue;

                    tileTemp.transform.position = new Vector3(xPos, yPos, 0);
                    tileTemp.name = $"HEX_{x}-{y}";
                    tileTemp.TileCoordinate = new Vector2Int(x, y);

                    if (onEditor)
                    {
                        tileTemp.gameObject.SetActive(true);
                        continue;
                    }


                    _tiles[x, y] = tileTemp;

                    if (levelDictionary.TryGetValue(new Vector2Int(x, y), out List<HexViewDto> viewData))
                    {
                        tileTemp.SetElementData(viewData);
                    }

                    tileTemp.gameObject.SetActive(true);
                }
            }

            if (onEditor) return;

            var camera = Camera.main;
            camera.transform.position = _gridBuilderDataContainer.LevelBaseSettings[0].CameraPos;
            camera.orthographicSize = _gridBuilderDataContainer.LevelBaseSettings[0].CameraSize;
        }

        /// <summary>
        ///  Remove all tiles from scene.
        /// </summary>
        /// <param name="onEditor">Specifies whether the grid is being built in the editor (true by default).</param>
        public void RemoveTiles(bool onEditor = true)
        {
            var childList = new List<TileBase>(transform.GetComponentsInChildren<TileBase>());

            foreach (var child in childList)
            {
                if (onEditor)
                {
                    DestroyImmediate(child.gameObject);
                    continue;
                }

                Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Retrieves the HexTile at the specified coordinates from the grid.
        /// Returns null if the coordinates are out of bounds.
        /// </summary>
        /// <param name="coordinate">The coordinates of the tile to retrieve.</param>
        /// <returns>The HexTile at the specified coordinates, or null if out of bounds.</returns>
        private HexTile GetTile(Vector2Int coordinate)
        {
            if (coordinate.x < 0 || coordinate.y < 0 || coordinate.x >= _tiles.GetLength(0) ||
                coordinate.y >= _tiles.GetLength(1))
            {
                return null;
            }

            return _tiles[coordinate.x, coordinate.y];
        }

#if UNITY_EDITOR
        /// <summary>
        /// Save to level data on SO.
        /// </summary>
        [Obsolete("Obsolete")]
        public void SaveLevelDataOnEditor()
        {
            ConvertListToMatrix(); // for editor bug

            var levelData = new List<LevelTilesView>();
            for (var i = 0; i < _tiles.GetLength(0); i++)
            {
                for (var j = 0; j < _tiles.GetLength(1); j++)
                {
                    var key = new Vector2Int(i, j);
                    var value = _tiles[i, j].GetElementsData();

                    levelData.Add(new LevelTilesView
                    {
                        Key = key,
                        Value = value
                    });
                }
            }

            var mainCamera = Camera.main;

            _gridBuilderDataContainer.LevelBaseSettings.Add(new LevelBaseSettings()
            {
                GridHeight = GridHeight,
                GridWidth = GridWidth,
                CameraSize = mainCamera.orthographicSize,
                CameraPos = mainCamera.transform.position,
                LevelTilesViews = levelData,
            });

            _gridBuilderDataContainer.SetDirty();

            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// Converts a list of HexTile objects into a 2D matrix based on the grid's width and height.
        /// </summary>
        private void ConvertListToMatrix()
        {
            _tiles = new HexTile[GridWidth, GridHeight];

            for (int i = 0; i < TileList.Count; i++)
            {
                var x = i / GridHeight;
                var y = i % GridHeight;

                _tiles[x, y] = TileList[i];
            }
        }
#endif
    }
}