using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "GameDB/Item")]
public class ItemDB : ScriptableObject
{
    public uint index;
    public string label;
    public uint weight = 1;
}
