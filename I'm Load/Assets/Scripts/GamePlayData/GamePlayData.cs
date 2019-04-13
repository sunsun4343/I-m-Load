using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayData : MonoBehaviour
{
    [SerializeField] Transform CameraTransform;
    [SerializeField] TilemapController tilemap_Ground;

    internal void GenerateMap()
    {

        float size = 100;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                tilemap_Ground.SetTile(new Vector2Int(x, y), 0);
            }
        }

        CameraTransform.position = new Vector3(size * 0.5f, size* 0.5f, -10);

    }
}
