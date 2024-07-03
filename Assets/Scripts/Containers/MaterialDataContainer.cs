using Enums;
using UnityEngine;

namespace Containers
{
    /// <summary>
    /// ScriptableObject for storing materials used to outline hex tile views.
    /// </summary>
    [CreateAssetMenu(fileName = "MaterialDataContainer", menuName = "ScriptableObjects/MaterialDataContainer")]
    public class MaterialDataContainer : ScriptableObject
    {
        [SerializeField] private Material[] _materials;

       public Material GetMaterial(ColorType colorType)
       {
           return _materials[(int)colorType];
       }
        
    }
}
