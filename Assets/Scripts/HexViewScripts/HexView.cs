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
        [SerializeField] private Direction _direction;
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
            var materials = _properties.OutLineMeshRenderer.materials;
            materials[0] = _properties.BaseMaterial;
            materials[1] = _properties.OutLineMaterialDataContainer.GetMaterial(colorType);
            _properties.OutLineMeshRenderer.materials = materials;
        }

        /// <summary>
        /// Sets the hex view properties based on the provided HexViewDto object. This is used to configure child settings when the game starts.
        /// </summary>
        /// <param name="dto">HexViewDto object containing the properties to set for the hex view.</param>
        public void SetHexView(HexViewDto dto)
        {

            _hexViewElement = Instantiate(_properties.HexViewElementPrefabs[(int)dto.HexViewElementType], _properties.HexViewTransform);
            _hexViewElement.MyHexView = this;
            _hexViewElement.transform.localPosition = new Vector3(0, 0, -0.025f);

            _hexViewElement.gameObject.name = $"{dto.HexViewElementType}_{dto.HexViewColorType}";
            
            _hexViewElementType = dto.HexViewElementType;
            _targetColorType = dto.HexViewColorType;
            
            _hexViewElement.SetDirection(dto.Direction);
            
            SetOutLineColor(_targetColorType);
            
            _hexViewElement.SetColor(_targetColorType);
            
       
            
        }

#if UNITY_EDITOR
        
        /// <summary>
        /// Creates and returns a HexViewDto object containing the current settings of the HexViewElement.
        /// </summary>
        /// <returns>HexViewDto for saving level settings.</returns>
        public HexViewDto GetViewDto()
        {
            var hexViewDto = new HexViewDto
            {
             HexViewElementType = _hexViewElementType,
             HexViewColorType = _targetColorType,
             Direction = _direction,
            };

            return hexViewDto;
        }
#endif
    }
}