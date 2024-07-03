using System.Collections.Generic;
using Containers;
using UnityEngine;

namespace HexViewScripts
{
    [System.Serializable]
    public struct HexViewData
    {
        public Transform HexViewTransform;
        public Material BaseMaterial;
        public MaterialDataContainer OutLineMaterialDataContainer;
        public MeshRenderer OutLineMeshRenderer;
        public List<HexViewElement> HexViewElementPrefabs;
    }
}