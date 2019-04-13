using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayData : MonoBehaviour
{
    [SerializeField] Transform CameraTransform;
    [SerializeField] TilemapController tilemap_Ground;
    [SerializeField] TilemapController tilemap_Build;

    uint[,] map_Ground;
    uint[,] map_Build;

    internal void GenerateMap()
    {
        int size = 100;

        map_Ground = new uint[size, size];
        map_Build = new uint[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                map_Ground[y, x] = 0;
                tilemap_Ground.SetTile(new Vector2Int(x, y), 0);
            }
        }

        CameraTransform.position = new Vector3(size * 0.5f, size* 0.5f, -10);

    }

    public void CreateBuilding(Building db, Vector2Int buildPosition, int rotate)
    {
        Vector2Int size = db.size;
        if (rotate == 1 || rotate == 3)
            size = new Vector2Int(db.size.y, db.size.x);

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                Vector2Int pos = buildPosition + new Vector2Int(x,size.y - 1 - y);
                switch (rotate)
                {
                    case 0:
                        tilemap_Build.SetTile(pos, db.tileBases_R0[y * size.x + x]);
                        break;
                    case 1:
                        tilemap_Build.SetTile(pos, db.tileBases_R1[y * size.x + x]);
                        break;
                    case 2:
                        tilemap_Build.SetTile(pos, db.tileBases_R2[y * size.x + x]);
                        break;
                    case 3:
                        tilemap_Build.SetTile(pos, db.tileBases_R3[y * size.x + x]);
                        break;
                }
                map_Build[pos.x, pos.y] = db.index;
            }
        }

    }
}
