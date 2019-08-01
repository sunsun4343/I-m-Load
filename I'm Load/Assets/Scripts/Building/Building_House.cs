using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building_House : Building
{
    public BuildingComponent_ResidenceCitizen residenceCitizenComponent { get; }
    public BuildingComponent_StorageItem storageItemComponent { get; }

    public Building_House(BuildingDB db, Vector2Int position, int rotate) : base(db, position, rotate)
    {
        storageItemComponent = new BuildingComponent_StorageItem(this);
        residenceCitizenComponent = new BuildingComponent_ResidenceCitizen(this);

    }

    public BuildingPath buildingPath_Storage;


}
