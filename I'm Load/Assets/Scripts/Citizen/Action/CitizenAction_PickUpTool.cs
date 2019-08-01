using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenAction_PickUpTool : CitizenAction
{
    enum State
    {
        FindPath,
        Move,
        PickUp
    }

    State state = State.FindPath;

    public CitizenAction_PickUpTool(Citizen citizen) : base(citizen)
    {

    }

    Building_Storage building_Storage;

    public override bool ExcuteAction(double deltaTime)
    {
        if(state == State.FindPath)
        {
            if (citizen.resident_house.buildingPath_Storage == null)
            {
                Building building = citizen.gamePlayData.FindNearBuilding(citizen.resident_house.position, BuildingDB.Category.Storage);
                citizen.resident_house.buildingPath_Storage = citizen.gamePlayData.FindPath(citizen.resident_house, building);
            }

            building_Storage = citizen.resident_house.buildingPath_Storage.targetBuilding as Building_Storage;
            excuteTime = citizen.resident_house.buildingPath_Storage.distance;
            state = State.Move;
        }

        if (state == State.Move)
        {
            excuteTime -= deltaTime;

            if(excuteTime <= 0)
            {
                state = State.PickUp;
            }
            else
            {
                return false;
            }
        }

        if (state == State.PickUp)
        {
            var items = building_Storage.storageItemComponent.PullItem(ItemDB.Category.Tool, 1);
            citizen.gamePlayData.UseItem(items[0]);
            citizen.toolItemDB = items[0].db;
            citizen.toolDurability = items[0].db.durability;
        }

        return true;
    }



}
