using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayData : MonoBehaviour
{

    #region Time



    #endregion

    #region Map

    [SerializeField] TilemapController tilemap_Ground;
    [SerializeField] TilemapController tilemap_Build;

    public bool[,] map_Able_Build { get; private set; }
    public bool[,] map_Able_Move { get; private set; }

    uint[,] map_Index_Ground;
    uint[,] map_Index_Build;

    internal void GenerateMap(uint size)
    {
        map_Able_Build = new bool[size, size];
        map_Able_Move = new bool[size, size];

        map_Index_Ground = new uint[size, size];
        map_Index_Build = new uint[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                map_Able_Build[y, x] = true;
                map_Able_Move[y, x] = true;

                map_Index_Ground[y, x] = 0;
                tilemap_Ground.SetTile(new Vector2Int(x, y), 0);
            }
        }        
    }

    public void CreateBuilding(BuildingDB db, Vector2Int buildPosition, int rotate)
    {
        //List Setting
        Building building = new Building();
        building.db = db;
        building.position = buildPosition;

        buildings.Add(building);
        if (buildingsByIndex.ContainsKey(db.index))
            buildingsByIndex[db.index].Add(building);
        else
        {
            buildingsByIndex[db.index] = new List<Building>();
            buildingsByIndex[db.index].Add(building);
        }

        //TileMap Setting
        Vector2Int size = db.size;
        if (rotate == 1)
            size = new Vector2Int(db.size.y, db.size.x);

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                Vector2Int pos = buildPosition + new Vector2Int(x, size.y - 1 - y);
                switch (rotate)
                {
                    case 0:
                        tilemap_Build.SetTile(pos, db.tileBases_R0[y * size.x + x]);
                        break;
                    case 1:
                        tilemap_Build.SetTile(pos, db.tileBases_R1[y * size.x + x]);
                        break;
                }
                map_Index_Build[pos.x, pos.y] = db.index;

                map_Able_Build[pos.x, pos.y] = false;
                map_Able_Move[pos.x, pos.y] = false;
            }
        }
    }

    #endregion

    #region Building

    public List<Building> buildings = new List<Building>();
    public Dictionary<uint, List<Building>> buildingsByIndex = new Dictionary<uint, List<Building>>();

    #endregion

}
