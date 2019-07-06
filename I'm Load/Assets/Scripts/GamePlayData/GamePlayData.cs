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

    public uint[,] map_Index_Ground { get; private set; }
    public uint[,] map_Index_Build { get; private set; }

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

        uint chunkCount = (size-1) / (uint)buildingChunkSize + 1;
        buildings_chunk = new List<Building>[chunkCount, chunkCount];
        for (int i = 0; i < chunkCount; i++)
        {
            for (int j = 0; j < chunkCount; j++)
            {
                buildings_chunk[i, j] = new List<Building>();
            }
        }

    }

    #endregion

    #region Building

    public List<Building> buildings = new List<Building>();
    public Dictionary<uint, List<Building>> buildingsByIndex = new Dictionary<uint, List<Building>>();
    public int buildingChunkSize = 32;
    public List<Building>[,] buildings_chunk = new List<Building>[1, 1];

    public Building CreateBuilding(BuildingDB db, Vector2Int buildPosition, int rotate)
    {
        //List Setting
        Building building;
        switch (db.type)
        {
            case BuildingDB.BuildingType.House:
                building = new Building_House();
                break;
            case BuildingDB.BuildingType.Storage:
                building = new Building_Storage();
                break;
            default:
                building = new Building();
                break;
        }

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

        Vector2Int chunkLeftTopPos = new Vector2Int(buildPosition.x / buildingChunkSize, buildPosition.y / buildingChunkSize);
        Vector2Int chunkLeftBottomPos = new Vector2Int(buildPosition.x / buildingChunkSize, (buildPosition.y + db.size.y) / buildingChunkSize);
        Vector2Int chunkRightTopPos = new Vector2Int((buildPosition.x + db.size.x) / buildingChunkSize, buildPosition.y / buildingChunkSize);
        Vector2Int chunkRightBottomPos = new Vector2Int((buildPosition.x + db.size.x) / buildingChunkSize, (buildPosition.y + db.size.y) / buildingChunkSize);
        HashSet<Vector2Int> chunkPosSet = new HashSet<Vector2Int>();
        if(chunkPosSet.Contains(chunkLeftTopPos) == false) chunkPosSet.Add(chunkLeftTopPos);
        if (chunkPosSet.Contains(chunkLeftBottomPos) == false) chunkPosSet.Add(chunkLeftBottomPos);
        if (chunkPosSet.Contains(chunkRightTopPos) == false) chunkPosSet.Add(chunkRightTopPos);
        if (chunkPosSet.Contains(chunkRightBottomPos) == false) chunkPosSet.Add(chunkRightBottomPos);
        foreach (var item in chunkPosSet)
        {
            buildings_chunk[item.x, item.y].Add(building);
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

        return building;
    }

    public Building FindBuilding(Vector2Int pos)
    {
        Vector2Int chunkPos = new Vector2Int(pos.x / buildingChunkSize, pos.y / buildingChunkSize);

        //Debug.Log("chunkPos " + chunkPos);

        for (int i = 0; i < buildings_chunk[chunkPos.x, chunkPos.y].Count; i++)
        {
            Building building = buildings_chunk[chunkPos.x, chunkPos.y][i];
            Rect rect = new Rect(building.position.x, building.position.y, building.db.size.x, building.db.size.y);
            if (rect.Contains(pos))
            {
                return building;
            }
        }
        return null;
    }

    #endregion

    #region Citizen

    public List<Citizen> citizens = new List<Citizen>();

    public Citizen CreateCitizen(string name, bool sex, byte age)
    {
        Citizen citizen = new Citizen();

        citizen.name = name;
        citizen.sex = sex;
        citizen.age = age;

        citizens.Add(citizen);

        return citizen;
    }

    #endregion
}
