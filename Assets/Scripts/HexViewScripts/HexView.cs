using System;
using Containers;
using Dtos;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace HexViewScripts
{
    public class HexView : MonoBehaviour
    {
        [SerializeField] private HexViewData _properties;


        [SerializeField] private HexViewElementType _hexViewElementType;
        [SerializeField] private ColorType _targetColorType;

        private HexViewElement _hexViewElement;

        /// <summary>
        /// Sets the outline color of the hex tile based on the specified color type.
        /// </summary>
        /// <param name="colorType">The type of color to set for the outline.</param>
        ///
        private void SetOutLineColor(ColorType colorType)
        {
            _properties.OutLineMeshRenderer.materials[1] =
                _properties.OutLineMaterialDataContainer.GetOutLineMaterial(colorType);
        }

        public void SetHexView(HexViewDto dto)
        {
            _hexViewElementType = dto.HexViewElementType;
            _targetColorType = dto.HexViewColorType;
            
            SetOutLineColor(_targetColorType);
            _hexViewElement.SetColor(_targetColorType);
        }

#if UNITY_EDITOR
        public HexViewDto GetViewDto()
        {
            var hexViewDto = new HexViewDto
            {
             HexViewElementType = _hexViewElementType,
             HexViewColorType = _targetColorType,
            };

            return hexViewDto;
        }
#endif
    }
}