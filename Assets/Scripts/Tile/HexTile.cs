using System;
using System.Collections.Generic;
using System.Linq;
using Dtos;
using Enums;
using Extensions;
using HexViewScripts;
using UnityEngine;

namespace Tile
{
    public class HexTile : TileBase
    {
        /// <summary>
        /// Returns the neighboring coordinate of a hex tile based on the given direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
#if UNITY_EDITOR
        [SerializeField]
#endif
        private List<HexView> _hexViews;

        [SerializeField] private HexTileData _properties;

        private void Awake()
        {
            _properties.EmpyViewObject.SetActive(_hexViews.Count == 0);
            
        }

        public override Vector2Int GetNeighbourByDirection(Direction direction)
        {
            return TileCoordinate.GetCoordinate(direction);
        }

        public void SetElementData(List<HexViewDto> hexViewDtos)
        {
            _hexViews = new List<HexView>();
            foreach (var hexViewDto in hexViewDtos)
            {
                var hexView = Instantiate(_properties.HexViewPrefab, _properties.HexViewsTransform);
                hexView.gameObject.SetActive(true);
                hexView.SetHexView(hexViewDto,TileCoordinate);
                hexView.transform.localPosition = new Vector3(0, 0, .15f * _hexViews.Count);
                _hexViews.Add(hexView);
            }
        }

        public HexView GetTopStackElement()
        {
            if (_hexViews.Count >= 1)
            {
                return _hexViews[^1];
            }

            return null;
        }

        public override void ScaleDown(float time)
        {
            
        }

#if UNITY_EDITOR
        public List<HexViewDto> GetElementsData()
        {
            return _hexViews.Select(hexView => hexView.GetViewDto()).ToList();
        }
#endif
    }
}