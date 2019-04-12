using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGenerator : MonoBehaviour
{
    [SerializeField] GamePlayData gamePlayData;


    void Start()
    {
        GameStartConfig config = FindGameStartConfig();

        gamePlayData.GenerateMap();

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
