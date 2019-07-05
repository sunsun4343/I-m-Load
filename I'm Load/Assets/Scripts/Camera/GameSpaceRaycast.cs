﻿using BitBenderGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameSpaceRaycast : MonoBehaviour
{
    [SerializeField] TouchInputController touchInputController;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Tilemap tilemap;
    [SerializeField] GamePlayData gamePlayData;

    Rect gameSpaceRect;

    private void Awake()
    {
        float Width = Screen.width;
        float Height = Screen.height;
        gameSpaceRect = rectTransform.rect;
        gameSpaceRect.x = rectTransform.localPosition.x;
        gameSpaceRect.y = Height - rectTransform.localPosition.y - rectTransform.rect.height;

        touchInputController.OnInputClick += TouchInputController_OnInputClick;
    }
    
    private void TouchInputController_OnInputClick(Vector3 clickPosition, bool isDoubleClick, bool isLongTap)
    {
        if (gameSpaceRect.Contains(clickPosition))
        {
            Ray ray = Camera.main.ScreenPointToRay(clickPosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);

            if (this.tilemap.transform == hit.transform)
            {
                int x, y;
                x = this.tilemap.WorldToCell(ray.origin).x;
                y = this.tilemap.WorldToCell(ray.origin).y;

                uint buildingIndex = gamePlayData.map_Index_Build[x, y];
                if (buildingIndex != 0)
                {
                    //Debug.Log("buildingIndex " + buildingIndex);

                    Building building = gamePlayData.FindBuilding(new Vector2Int(x, y));

                    Debug.Log("Build " + building.position);
                }
                
            }
        }

    }




}