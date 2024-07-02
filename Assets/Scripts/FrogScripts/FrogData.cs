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
        public ColorType FrogColorType;
        [SerializeField] private SkinnedMeshRenderer _frogMeshRenderer;
        [SerializeField] public MaterialDataContainer _frogMaterialData;

        /// <summary>
        /// Sets the color of the frog based on its color type.
        /// </summary>
        public void SetFrogColor()
        {
            _frogMeshRenderer.material = _frogMaterialData.GetOutLineMaterial(FrogColorType);
        }
    }
}