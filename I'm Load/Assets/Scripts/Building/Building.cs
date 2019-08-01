using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    public BuildingDB db;
    public Vector2Int position;
    public int rotate;

    List<Vector2Int> Positions_R0;
    List<Vector2Int> Positions_R1;

    public Building(BuildingDB db, Vector2Int position, int rotate)
    {
        this.db = db;
        this.position = position;
        this.rotate = rotate;

        Init_Positions();
    }

    void Init_Positions()
    {
        int count = db.size.x * db.size.y;
        Positions_R0 = new List<Vector2Int>(count);
        Positions_R1 = new List<Vector2Int>(count);

        for (int y = 0; y < db.size.y; y++)
        {
            for (int x = 0; x < db.size.x; x++)
            {
                Positions_R0.Add(position + new Vector2Int(x, db.size.y - 1 - y));
            }
        }

        for (int y = 0; y < db.size.x; y++)
        {
            for (int x = 0; x < db.size.y; x++)
            {
                Positions_R1.Add(position + new Vector2Int(x, db.size.x - 1 - y));
            }
        }

    }

    public Vector2Int[] GetBuildingPositions()
    {
        switch (rotate)
        {
            case 0: return Positions_R0.ToArray();
            case 1:return Positions_R1.ToArray();
        }
        return null;
    }
}
