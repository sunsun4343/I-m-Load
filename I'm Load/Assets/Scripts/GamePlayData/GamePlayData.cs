using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayData : MonoBehaviour
{
    [SerializeField] TilemapController tilemap_Ground;

    internal void GenerateMap()
    {
        for (int y = -50; y <= 50; y++)
        {
            for (int x = -50; x <= 50; x++)
            {
                tilemap_Ground.SetTile(new Vector2Int(x, y), 0);
            }
        }

    }
}
