using Containers;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace OrientationScripts
{
    [System.Serializable]
    public class OrientationData 
    {
        [SerializeField] private MeshRenderer _orientationMeshRenderer;

        [SerializeField]
        private MaterialDataContainer _materialDataContainer;

        /// <summary>
        /// Sets the color of the orientation based on its color type.
        /// </summary>
        public void SetOrientationColor(ColorType colorType)
        {
            _orientationMeshRenderer.material = _materialDataContainer.GetMaterial(colorType);
        }
    }
}
