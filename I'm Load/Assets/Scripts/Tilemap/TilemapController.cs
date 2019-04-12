using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    Tilemap tilemap;

    [SerializeField] TileBase[] tileBases;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void SetTile(Vector2Int position, int index)
    {
        Vector3Int pos = new Vector3Int(position.x, position.y, 0);
        tilemap.SetTile(pos, tileBases[index]);
    }

}
