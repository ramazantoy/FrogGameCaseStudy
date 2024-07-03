using Containers;
using Enums;
using UnityEngine;

namespace FrogScripts
{
    /// <summary>
    /// Struct for storing and managing data related to a frog, including its animator, color type, and mesh renderer.
    /// </summary>
    [System.Serializable]
    public struct FrogData
    {
        public Animator FrogAnimator;
        [SerializeField] private SkinnedMeshRenderer _frogMeshRenderer;
        [SerializeField] private MaterialDataContainer _frogMaterialData;

        /// <summary>
        /// Sets the color of the frog based on its color type.
        /// </summary>
        public void SetFrogColor(ColorType colorType)
        {
            _frogMeshRenderer.material = _frogMaterialData.GetMaterial(colorType);
        }
    }
}