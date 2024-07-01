
using Enums;
using UnityEngine;

namespace HexViewScripts
{
    /// <summary>
    /// ScriptableObject for storing materials used to outline hex tile views.
    /// </summary>
    [CreateAssetMenu(fileName = "HexViewDataContainer", menuName = "ScriptableObjects/HexViewDataContainer")]
    public class HexViewDataContainer : ScriptableObject
    { 
        [SerializeField]
       private Material[] _outlineMaterials;

       public Material GetOutLineMaterial(ColorType colorType)
       {
           return _outlineMaterials[(int)colorType];
       }
        
    }
}
