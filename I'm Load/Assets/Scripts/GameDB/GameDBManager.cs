using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDBManager : SingletonBehaviour<GameDBManager>
{
    public List<BuildingDB> buildingDBs = new List<BuildingDB>();
    public List<ItemDB> itemDBs = new List<ItemDB>();
    public List<JobDB> jobDBs = new List<JobDB>();

}
