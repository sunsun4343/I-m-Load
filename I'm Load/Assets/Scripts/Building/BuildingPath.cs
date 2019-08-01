using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingPath 
{
    public Building targetBuilding;
    public float distance;
    public List<Vector2Int> path;
}
