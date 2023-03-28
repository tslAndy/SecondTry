using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap map;

    [SerializeField] private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> _dataFromTiles;

    private void Awake()
    {
        _dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (TileData tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
                _dataFromTiles.Add(tile, tileData);
        }
    }

    public float GetSpeedFactor(Vector2 position)
    {
        Vector3Int gridPos = map.WorldToCell(position);
        TileBase tile = map.GetTile(gridPos);
        if (tile == null || !_dataFromTiles.ContainsKey(tile))
            return 1;
        float speedFactor = _dataFromTiles[tile].speedModifier;
        return speedFactor;
    }

}
