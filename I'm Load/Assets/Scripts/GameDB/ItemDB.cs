using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "GameDB/Item")]
public class ItemDB : ScriptableObject
{
    public enum Category
    {
        None,
        Material,
        Tool,
        Max
    }
    public Category category;

    public enum Type
    {
        None,
        Wood,
        Stone,
        IronOre,
        Iron,
        StoneTool,
        IronTool,
    }
    public Type type;

    public string labelKey;
    public uint weight = 1;
    public Sprite iconSprite;

    /// <summary>
    /// 아이템 내구도
    /// </summary>
    public uint durability;
}
