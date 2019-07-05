using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_House : Building
{

    public List<Citizen> residence_citizen = new List<Citizen>();

    public void MoveInCitizen(Citizen citizen)
    {
        residence_citizen.Add(citizen);
    }
}
