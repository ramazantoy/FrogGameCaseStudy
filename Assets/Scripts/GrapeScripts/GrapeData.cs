using Containers;
using Enums;
using UnityEngine;

namespace GrapeScripts
{
    [System.Serializable]
    public class GrapeData
    {
        [SerializeField] private MeshRenderer _grapeMeshRenderer;

        [SerializeField]
        private MaterialDataContainer _materialDataContainer;

        /// <summary>
        /// Sets the color of the grape based on its color type.
        /// </summary>
        public void SetGrapeColor(ColorType colorType)
        {
            _grapeMeshRenderer.material = _materialDataContainer.GetMaterial(colorType);
        }
    }
}