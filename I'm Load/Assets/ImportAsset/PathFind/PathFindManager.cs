using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindManager : MonoBehaviour
{
    #region A*

    public class Result
    {
        public List<Vector2Int> path;
        public uint cost;
    }

    class Tile
    {
        public Vector2Int pos;
        public uint f;
        public uint g;
        public uint h;
        public Tile next;

        public Tile(Vector2Int pos, Vector2Int end)
        {
            this.pos = pos;
            this.g = 0;
            this.h = (uint)(Mathf.Abs(end.x - pos.x) + Mathf.Abs(end.y - pos.y));
            this.f = g + h;
        }
    }

    Vector2Int[] Directions = new Vector2Int[] { new Vector2Int(0,1), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(-1, 0) };

    public Result FindPath(Vector2Int start, Vector2Int end, uint[,] map)
    {
        int hight = map.GetLength(0);
        int width = map.GetLength(1);
        
        List<Tile> openlist = new List<Tile>();
        List<Tile> closelist = new List<Tile>();
        bool[,] openMap = new bool[hight, width];
        bool[,] closeMap = new bool[hight, width];
        Tile[,] tileMap = new Tile[hight, width];

        //시작위치 추가
        Tile startTile = new Tile(start, end);
        openlist.Add(startTile);
        openMap[start.y, start.x] = true;
        tileMap[start.y, start.x] = startTile;

        while (openlist.Count > 0)
        {
            //가장 비용이 낮은 타일 획득
            Tile tile = openlist[0];
            for (int i = 1; i < openlist.Count; i++)
            {
                if (tile.f > openlist[i].f) tile = openlist[i];
            }

            //획득한 타일이 목적지라면 종료
            if (tile.pos == end) break;

            openlist.Remove(tile);
            openMap[start.y, start.x] = false;            
            closelist.Add(tile);
            closeMap[tile.pos.y, tile.pos.x] = true;

            //주변 타일 OpenList 추가
            for (int i = 0; i < Directions.Length; i++)
            {
                Vector2Int newPos = tile.pos + Directions[i];
                
                //맵 바깥이라면 생략
                if (newPos.x < 0) continue;
                if (newPos.y < 0) continue;
                if (newPos.x >= width) continue;
                if (newPos.y >= hight) continue;

                //이동불가 타일이라면 생략
                if (map[newPos.y, newPos.x] == 0) continue;

                //Close된 타일이라면 생략
                if (closeMap[newPos.y, newPos.x]) continue;

                //OpenList에 없다면
                if(openMap[newPos.y, newPos.x] == false)
                {
                    Tile newTile = new Tile(newPos, end);
                    newTile.g = tile.g + map[newPos.y, newPos.x];
                    newTile.h = (uint)(Mathf.Abs(end.x - newPos.x) + Mathf.Abs(end.y - newPos.y));
                    newTile.f = newTile.g + newTile.h;
                    newTile.next = tile;

                    //OpenList 추가
                    openlist.Add(newTile);
                    openMap[newPos.y, newPos.x] = true;
                    tileMap[newPos.y, newPos.x] = newTile;
                }
                //OpenList에 있고, 신규 경로가 비용이 더 싸다면 
                else if (tileMap[newPos.y, newPos.x].g > tile.g + map[newPos.y, newPos.x])
                {
                    Tile newTile = tileMap[newPos.y, newPos.x];
                    newTile.g = tile.g + map[newPos.y, newPos.x];
                    newTile.f = newTile.g + newTile.h;
                    newTile.next = tile;
                }
            }
        }

        //경로 산출
        Result result = new Result();
        result.path = new List<Vector2Int>();
        Tile pathTile = tileMap[end.y, end.x];
        while (pathTile != null)
        {
            result.path.Add(pathTile.pos);

            if (pathTile.next.Equals(startTile))
                break;

            pathTile = pathTile.next;
            result.cost += map[pathTile.pos.y, pathTile.pos.x];
        }
        result.path.Reverse();

        return result;

    }

    #endregion


}
