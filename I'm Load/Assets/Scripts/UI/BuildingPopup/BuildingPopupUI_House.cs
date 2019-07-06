using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPopupUI_House : BuildingPopupUI
{
    [SerializeField] BuildingPopupItemUI_HouseCitizen ItemPrefabs;
    [SerializeField] Transform ItemParent;

    List<BuildingPopupItemUI_HouseCitizen> ItemUIs = new List<BuildingPopupItemUI_HouseCitizen>();

    public void Show(Building_House building)
    {
        for (int i = 0; i < ItemUIs.Count; i++)
        {
            Lean.Pool.LeanPool.Despawn(ItemUIs[i]);
        }
        ItemUIs.Clear();

        for (int i = 0; i < building.residence_citizen.Count; i++)
        {
            var item = Lean.Pool.LeanPool.Spawn<BuildingPopupItemUI_HouseCitizen>(ItemPrefabs);
            item.transform.parent = ItemParent;
            item.gameObject.SetActive(true);
            item.InitUI(building.residence_citizen[i]);
            ItemUIs.Add(item);
        }

        this.gameObject.SetActive(true);
    }
}
