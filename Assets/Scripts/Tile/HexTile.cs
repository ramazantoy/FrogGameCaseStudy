using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Dtos;
using Enums;
using Events.EventBusScripts;
using Events.GameEvents;
using Extensions;
using HexViewScripts;
using UnityEngine;

namespace Tile
{
   public class HexTile : TileBase
{
    /// <summary>
    /// List of hex views attached to the tile.
    /// </summary>
#if UNITY_EDITOR
    [SerializeField]
#endif
    private List<HexView> _hexViews;

    [SerializeField] private HexTileData _properties;

    private void Awake()
    {
        // Activate the empty view object if there are no hex views present.
        _properties.EmpyViewObject.SetActive(_hexViews.Count == 0);
    }

    /// <summary>
    /// Returns the neighboring coordinate of a hex tile based on the given direction.
    /// </summary>
    /// <param name="direction">Direction of the neighboring tile.</param>
    /// <returns>Coordinate of the neighboring tile.</returns>
    public override Vector2Int GetNeighbourByDirection(Direction direction)
    {
        return TileCoordinate.GetCoordinate(direction);
    }

    /// <summary>
    /// Sets the hex view elements on the tile based on the provided HexViewDto list.
    /// </summary>
    /// <param name="hexViewDtos">List of HexViewDto objects containing the properties for each hex view.</param>
    public void SetElementData(List<HexViewDto> hexViewDtos)
    {
        _hexViews = new List<HexView>();
        foreach (var hexViewDto in hexViewDtos)
        {
            var hexView = Instantiate(_properties.HexViewPrefab, _properties.HexViewsTransform);
            hexView.gameObject.SetActive(true);
            hexView.SetHexView(hexViewDto, TileCoordinate);
            hexView.transform.localPosition = new Vector3(0, 0, .25f * _hexViews.Count);
            _hexViews.Add(hexView);
        }
        
        _hexViews[0].Ready();
    }

    /// <summary>
    /// Retrieves the topmost hex view element from the stack of hex views.
    /// </summary>
    /// <returns>Topmost HexView element.</returns>
    public HexView GetTopStackElement()
    {
        if (_hexViews.Count >= 1)
        {
            return _hexViews[0];
        }

        return null;
    }

    /// <summary>
    /// Asynchronously blows away the top hex view element with animation.
    /// </summary>
    public async UniTaskVoid BlowTopElement()
    {
        if (_hexViews.Count >= 1)
        {
            await _hexViews[0].BlowYourSelf();
            
            _hexViews.RemoveAt(0);
        }

        
        RePosHexViews();
    }

    /// <summary>
    /// Repositions the hex views after modifying the stack.
    /// </summary>
    private void RePosHexViews()
    {
        EventBus<OnNewHexEvent>.Publish(new OnNewHexEvent());
        if (_hexViews.Count <= 0)
        {
            _properties.EmpyViewObject.SetActive(true);
            return;
        }
        for (var i = 0; i < _hexViews.Count; i++)
        {
            _hexViews[i].transform.DOLocalMove(new Vector3(0, 0, .25f) * i,.25f);
        }
        _hexViews[0].WakeUp();
    }

    /// <summary>
    /// Placeholder for scaling down the tile (currently not implemented).
    /// </summary>
    /// <param name="time">Time duration for scaling.</param>
    public override void ScaleDown(float time)
    {
        // Placeholder method for scaling down the tile, currently not implemented.
    }

    public bool HaveStackElement => _hexViews.Count > 0 ? true : false;
#if UNITY_EDITOR
    /// <summary>
    /// Retrieves the data of all hex view elements attached to the tile.
    /// </summary>
    /// <returns>List of HexViewDto objects representing the data of hex views.</returns>
    public List<HexViewDto> GetElementsData()
    {
        return _hexViews.Select(hexView => hexView.GetViewDto()).ToList();
    }
#endif
}
}