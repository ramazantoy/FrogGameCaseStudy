using Enums;
using UnityEngine;

namespace HexViewScripts
{
    public class HexView : MonoBehaviour
    {
        [SerializeField]
        private HexViewDataContainer _hexViewDataContainer;
        
        [SerializeField]
        private MeshRenderer _outLineMeshRenderer;
        
        /// <summary>
        /// Sets the outline color of the hex tile based on the specified color type.
        /// </summary>
        /// <param name="colorType">The type of color to set for the outline.</param>

        public void SetOutLineColor(ColorType colorType)
        {
            _outLineMeshRenderer.material = _hexViewDataContainer.GetOutLineMaterial(colorType);
        }
    }
}
