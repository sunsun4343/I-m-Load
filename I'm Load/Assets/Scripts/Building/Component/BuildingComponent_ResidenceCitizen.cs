using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingComponent_ResidenceCitizen
{
    public Building building;
    public List<Citizen> residence_citizen = new List<Citizen>();

    public BuildingComponent_ResidenceCitizen(Building building)
    {
        this.building = building;
    }

    public void MoveInCitizen(Citizen citizen)
    {
        residence_citizen.Add(citizen);
        citizen.resident_house = building as Building_House;
    }
}
