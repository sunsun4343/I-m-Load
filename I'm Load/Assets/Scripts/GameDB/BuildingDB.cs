using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="New Building", menuName ="GameDB/Building")]
public class BuildingDB : ScriptableObject
{
    public uint index;
    public string label;
    public Sprite[] PreviewSprites;
    public Vector2Int size;
    public Vector2Int offsetSize
    {
        get
        {
            return new Vector2Int( Mathf.CeilToInt(size.x / 2), Mathf.CeilToInt(size.y / 2));
        }
    }

    public TileBase[] tileBases_R0;
    public TileBase[] tileBases_R1;

    public enum BuildingType
    {
        None,
        House,
    }

    public BuildingType type;

}
