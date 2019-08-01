using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayData : MonoBehaviour
{
    [SerializeField] GameDBManager gameDB;
    public PathFindManager pathFindManager;

    private void Awake()
    {
        Initialized_Itme();
    }

    #region Time



    #endregion

    #region Map

    [SerializeField] TilemapController tilemap_Ground;
    [SerializeField] TilemapController tilemap_Build;

    public uint MapSize { get; private set; }
    public bool[,] map_Able_Build { get; private set; }
    public bool[,] map_Able_Move { get; private set; }

    public uint[,] map_Index_Ground { get; private set; }

    public uint[,] map_Index_Build { get; private set; }
    
    /// <summary>
    /// 이동에 필요한 코스트정보를 가진 맵
    /// </summary>
    public uint[,] map_MoveCost { get; private set; }

    internal void GenerateMap(uint size)
    {
        MapSize = size;

        map_Able_Build = new bool[size, size];
        map_Able_Move = new bool[size, size];

        map_Index_Ground = new uint[size, size];
        map_Index_Build = new uint[size, size];
        map_MoveCost = new uint[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                map_Able_Build[y, x] = true;
                map_Able_Move[y, x] = true;

                map_Index_Ground[y, x] = 0;
                tilemap_Ground.SetTile(new Vector2Int(x, y), 0);

                map_MoveCost[y, x] = 10;
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

    public BuildingPath FindPath(Building start, Building end)
    {
        Vector2Int startPosition = start.position;
        Vector2Int endPosition = end.position;

        //해당 빌딩 이동코스트 맵 수정
        uint[,] moveCostMap = map_MoveCost.Clone() as uint[,];
        
        var startPositions = start.GetBuildingPositions();
        for (int i = 0; i < startPositions.Length; i++)
        {
            map_MoveCost[startPositions[i].y, startPositions[i].x] = 10;
        }
        var endPositions = end.GetBuildingPositions();
        for (int i = 0; i < endPositions.Length; i++)
        {
            map_MoveCost[endPositions[i].y, endPositions[i].x] = 10;
        }

        //경로 찾기
        var result = pathFindManager.FindPath(startPosition, endPosition, map_MoveCost);

        BuildingPath buildingPath = new BuildingPath();
        buildingPath.targetBuilding = end;
        buildingPath.path = result.path;
        buildingPath.distance = result.cost;

        return buildingPath;
    }

    #endregion

    #region Building

    /// <summary>
    /// 모든 빌딩 목록
    /// </summary>
    public List<Building> buildings = new List<Building>();
    /// <summary>
    /// 타입별 빌딩 목록
    /// </summary>
    public Dictionary<BuildingDB.Type, List<Building>> buildingsByType = new Dictionary<BuildingDB.Type, List<Building>>();
    /// <summary>
    /// 카테고리별 빌딩 목록
    /// </summary>
    public Dictionary<BuildingDB.Category, List<Building>> buildingsByCategory = new Dictionary<BuildingDB.Category, List<Building>>();
    public int buildingChunkSize = 32;
    /// <summary>
    /// 위치 청크별 빌딩목록
    /// </summary>
    public List<Building>[,] buildings_chunk;

    public Building CreateBuilding(BuildingDB db, Vector2Int buildPosition, int rotate)
    {
        //빌딩 타입에 따른 인스턴스 생성
        Building building;
        switch (db.category)
        {
            case BuildingDB.Category.House:
                building = new Building_House(db, buildPosition, rotate);
                break;
            case BuildingDB.Category.Storage:
                building = new Building_Storage(db, buildPosition, rotate);
                break;
            default:
                building = new Building(db, buildPosition, rotate);
                break;
        }

        //빌딩 목록 추가
        buildings.Add(building);

        //빌딩 타입별 목록 추가
        if (buildingsByType.ContainsKey(db.type))
            buildingsByType[db.type].Add(building);
        else
        {
            buildingsByType[db.type] = new List<Building>();
            buildingsByType[db.type].Add(building);
        }

        //빌딩 카테고리별 목록 추가
        if (buildingsByCategory.ContainsKey(db.category))
        {
            buildingsByCategory[db.category].Add(building);
        }
        else
        {
            var list = new List<Building>();
            buildingsByCategory.Add(db.category, list);
            list.Add(building);
        }

        //위치에따른 청크별 목록 추가
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

        //이미지 설정 / Map 설정

        Vector2Int[] buildingPositions = building.GetBuildingPositions();
        for (int i = 0; i < buildingPositions.Length; i++)
        {
            Vector2Int pos = buildingPositions[i];
            switch (rotate)
            {
                case 0:
                    tilemap_Build.SetTile(pos, db.tileBases_R0[i]);
                    break;
                case 1:
                    tilemap_Build.SetTile(pos, db.tileBases_R1[i]);
                    break;
            }

            //빌딩 인덱스맵 설정
            map_Index_Build[pos.y, pos.x] = (uint)db.type;

            //빌드가능맵 설정
            map_Able_Build[pos.y, pos.x] = false;

            //이동가능맵 설정
            map_Able_Move[pos.y, pos.x] = false;

            //이동 불가 코스트 설정
            map_MoveCost[pos.y, pos.x] = 0;
        }

        return building;
    }

    /// <summary>
    /// 해당 위치에 해당하는 빌딩을 찾습니다. 없다면 null을 반환합니다.
    /// </summary>
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

    /// <summary>
    /// 해당 위치에서 가장 가까운 빌딩을 찾습니다. 조건 : 카테고리
    /// </summary>
    public Building FindNearBuilding(Vector2Int position, BuildingDB.Category category)
    {
        if (buildingsByCategory.ContainsKey(category) == false) return null;

        var list = buildingsByCategory[category];

        uint minDist = uint.MaxValue;
        Building nearBuilding = null;
        for (int i = 0; i < list.Count; i++)
        {
            uint dist = Util.Distance(position, list[i].position);
            if(minDist > dist)
            {
                minDist = dist;
                nearBuilding = list[i];
            }
        }
        return nearBuilding;
    }

    #endregion

    #region Citizen

    public List<Citizen> citizens = new List<Citizen>();

    public Citizen CreateCitizen(string name, bool sex, byte age)
    {
        Citizen citizen = new Citizen(this);

        citizen.name = name;
        citizen.sex = sex;
        citizen.age = age;
        citizen.job = gameDB.jobDBs[(int)JobDB.Type.Worker];
        citizen.InitState();

        citizens.Add(citizen);

        return citizen;
    }

    #endregion

    #region Item

    public Dictionary<ItemDB.Category, List<Item>> ItemsByCategory = new Dictionary<ItemDB.Category, List<Item>>(); 
    public Dictionary<ItemDB.Type, Item> ItemByType = new Dictionary<ItemDB.Type, Item>();

    void Initialized_Itme()
    {
        for (int i = 0; i < (int)ItemDB.Category.Max; i++)
        {
            ItemsByCategory.Add((ItemDB.Category)i, new List<Item>());
        }
    }

    public Item GetItemSurely(ItemDB.Type type)
    {
        if (ItemByType.ContainsKey(type))
        {
            return ItemByType[type];
        }
        else
        {
            Item dicItem = new Item();
            dicItem.db = gameDB.itemDBs[(int)type];
            dicItem.count = 0;
            ItemByType[type] = dicItem;
            return dicItem;
        }
    }

    public Item CreateItem(ItemDB.Type type, int count)
    {
        Item item = new Item();
        item.db = gameDB.itemDBs[(int)type];
        item.count = count;

        if (ItemByType.ContainsKey(type))
        {
            ItemByType[type].count += count;
        }
        else
        {
            //타입별 아이템 목록에 추가
            Item dicItem = new Item();
            dicItem.db = item.db;
            dicItem.count = count;
            ItemByType[type] = dicItem;

            //카테고리별 아이템 목록에 추가
            var list = ItemsByCategory[item.db.category];
            list.Add(dicItem);
        }

        return item;
    }

    /// <summary>
    /// 카테고리에 해당하는 아이템의 수량이 창고에 남아있는가?
    /// </summary>
    public bool isStockItem(ItemDB.Category category)
    {
        var list = ItemsByCategory[category];
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].count > 0) return true;
        }
        return false;
    }

    /// <summary>
    /// 아이템을 사용한다. 아이템이 사라진다.
    /// </summary>
    public void UseItem(Item item)
    {
        ItemByType[item.db.type].count -= item.count;
    }
    

    #endregion

}
