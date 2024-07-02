
using System.Collections.Generic;
using Enums;
using HexViewScripts;
using UnityEngine;

namespace Containers
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "HexViewElementDataContainer", menuName = "ScriptableObjects/HexViewElementDataContainer")]
    public class HexViewElementsDataContainer : ScriptableObject
    {
        [SerializeField]
        private List<HexViewElement> _hexViewElementPrefabs;

        public HexViewElement GetViewPrefab(HexViewElementType elementType)
        {
            return _hexViewElementPrefabs[(int)elementType];
        }
    }
}
