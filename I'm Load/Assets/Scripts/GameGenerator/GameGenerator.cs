using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGenerator : MonoBehaviour
{
    [SerializeField] GamePlayData gamePlayData;
    [SerializeField] Transform CameraTransform;
    
    void Start()
    {
        GameStartConfig config = FindGameStartConfig();

        uint size = 100;
        gamePlayData.GenerateMap(size);

        Vector2Int centerPos = new Vector2Int((int)(size * 0.5f), (int)(size * 0.5f));

        CameraTransform.position = new Vector3(centerPos.x, centerPos.y, -10);

        GameDefaultMapGeneratorData gameDefaultMapGeneratorData = this.GetComponent<GameDefaultMapGeneratorData>();

        var woodHouseDB = gameDefaultMapGeneratorData.buildingDB_1_WoodHouse;
        var build_house0 = gamePlayData.CreateBuilding(woodHouseDB, centerPos + new Vector2Int(-10, 0), 0) as Building_House;
        build_house0.MoveInCitizen(gamePlayData.CreateCitizen(NameUtil.name(true), true, (byte)UnityEngine.Random.Range(10,18)));
        build_house0.MoveInCitizen(gamePlayData.CreateCitizen(NameUtil.name(false), false, (byte)UnityEngine.Random.Range(10, 18)));
        var build_house1 = gamePlayData.CreateBuilding(woodHouseDB, centerPos + new Vector2Int(-10, -5), 0) as Building_House;
        build_house1.MoveInCitizen(gamePlayData.CreateCitizen(NameUtil.name(true), true, (byte)UnityEngine.Random.Range(10, 18)));
        build_house1.MoveInCitizen(gamePlayData.CreateCitizen(NameUtil.name(false), false, (byte)UnityEngine.Random.Range(10, 18)));
        var build_house2 = gamePlayData.CreateBuilding(woodHouseDB, centerPos + new Vector2Int(-5, -10), 0) as Building_House;
        build_house2.MoveInCitizen(gamePlayData.CreateCitizen(NameUtil.name(true), true, (byte)UnityEngine.Random.Range(10, 18)));
        build_house2.MoveInCitizen(gamePlayData.CreateCitizen(NameUtil.name(false), false, (byte)UnityEngine.Random.Range(10, 18)));
        var build_house3 = gamePlayData.CreateBuilding(woodHouseDB, centerPos + new Vector2Int(0, -10), 0) as Building_House;
        build_house3.MoveInCitizen(gamePlayData.CreateCitizen(NameUtil.name(true), true, (byte)UnityEngine.Random.Range(10, 18)));
        build_house3.MoveInCitizen(gamePlayData.CreateCitizen(NameUtil.name(false), false, (byte)UnityEngine.Random.Range(10, 18)));
        var build_house4 = gamePlayData.CreateBuilding(woodHouseDB, centerPos + new Vector2Int(-5, 5), 0) as Building_House;
        build_house4.MoveInCitizen(gamePlayData.CreateCitizen(NameUtil.name(true), true, (byte)UnityEngine.Random.Range(10, 18)));
        build_house4.MoveInCitizen(gamePlayData.CreateCitizen(NameUtil.name(false), false, (byte)UnityEngine.Random.Range(10, 18)));
        var build_house5 = gamePlayData.CreateBuilding(woodHouseDB, centerPos + new Vector2Int(5, -5), 0) as Building_House;
        build_house5.MoveInCitizen(gamePlayData.CreateCitizen(NameUtil.name(true), true, (byte)UnityEngine.Random.Range(10, 18)));
        build_house5.MoveInCitizen(gamePlayData.CreateCitizen(NameUtil.name(false), false, (byte)UnityEngine.Random.Range(10, 18)));
        var wareHouseDB = gameDefaultMapGeneratorData.buildingDB_2_WareHouse;
        var build_wareHouse = gamePlayData.CreateBuilding(wareHouseDB, centerPos + new Vector2Int(0, 5), 1);

    }

    private GameStartConfig FindGameStartConfig()
    {
        GameStartConfig[] configs = FindObjectsOfType<GameStartConfig>();
        GameStartConfig config  = null;
        if (configs.Length == 1)
        {
            config = configs[0];
        }
        else
        {
            foreach (var item in configs)
            {
                if (item.name.Contains("Debug") == false)
                {
                    config = item;
                    break;
                }
            }
        }
        return config;
    }
}
