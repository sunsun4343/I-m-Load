using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Storage : Building
{
    public BuildingComponent_StorageItem storageItemComponent { get; }

    public Building_Storage(BuildingDB db, Vector2Int position, int rotate) : base(db, position, rotate)
    {
        storageItemComponent = new BuildingComponent_StorageItem(this);

    }

}
