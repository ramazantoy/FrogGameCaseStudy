using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TileBase = Tile.TileBase;

namespace GridSystem
{
    public class GridBuilder : MonoBehaviour
    {
        public float xOffset = 0.8f; 
        public float yOffset = 0.866f; 
        public GameObject tilePref;
        public Vector3 startPosition = new Vector3(0, 0, 0);

        public int gridWidth = 5;
        public int gridHeight = 5;
        
        public void BuildGrid(bool onEditor=true)
        {
            for (var x = 0; x < gridWidth; x++)
            {
                var xPos = startPosition.x + xOffset * x;
                for (var y = 0; y < gridHeight; y++)
                {
                    var yPos = startPosition.y - yOffset * y - (x % 2 == 0 ? 0 : yOffset / 2);
                    var tileTemp = onEditor ? PrefabUtility.InstantiatePrefab(tilePref,transform) as GameObject: Instantiate(tilePref, transform);
                    tileTemp.transform.position = new Vector3(xPos, yPos, startPosition.z);
                    tileTemp.name = $"HEX_{x}-{y}";
                }
            }
        }

        public void RemoveTiles(bool onEditor=true)
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
    }
}
