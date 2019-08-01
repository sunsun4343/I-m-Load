using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="New Building", menuName ="GameDB/Building")]
public class BuildingDB : ScriptableObject
{
    public enum Category
    {
        None,
        House,
        Storage,
    }
    public enum Type
    {
        None,
        WoodHouse,
        WoodStorage,
    }

    [Header("-구분")]
    public Category category;
    public Type type;

    [Header("-표시")]
    public string labelKey;
    public Texture[] PreviewTextures;
    public TileBase[] tileBases_R0;
    public TileBase[] tileBases_R1;

    [Header("-크기")]
    public Vector2Int size;
    public Vector2Int offsetSize
    {
        get
        {
            return new Vector2Int(Mathf.CeilToInt(size.x / 2), Mathf.CeilToInt(size.y / 2));
        }
    }

    [Header("-건설 조건")]
    public List<ItemCondition> ConstructionItemConditions = new List<ItemCondition>();

    [Header("-건설 정보")]
    public uint laborforce;


}
