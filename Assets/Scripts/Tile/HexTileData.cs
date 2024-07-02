

using HexViewScripts;
using UnityEngine;

namespace Tile
{
    [System.Serializable]
    public struct HexTileData
    {
        public GameObject EmpyViewObject;
        public Transform HexViewsTransform;
        public HexView HexViewPrefab;
    }
}
