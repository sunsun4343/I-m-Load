using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPopupUI_Storage : BuildingPopupUI
{
    [SerializeField] BuildingPopupItemUI_StorageItem ItemPrefabs;
    [SerializeField] Transform ItemParent;
    [SerializeField] Slider Slider_Storage;

    List<BuildingPopupItemUI_StorageItem> ItemUIs = new List<BuildingPopupItemUI_StorageItem>();

    public void Show(Building_Storage building)
    {
        for (int i = 0; i < ItemUIs.Count; i++)
        {
            Lean.Pool.LeanPool.Despawn(ItemUIs[i]);
        }
        ItemUIs.Clear();

        for (int i = 0; i < building.storage_items.Count; i++)
        {
            var item = Lean.Pool.LeanPool.Spawn<BuildingPopupItemUI_StorageItem>(ItemPrefabs);
            item.transform.parent = ItemParent;
            item.gameObject.SetActive(true);
            item.InitUI(building.storage_items[i]);
            ItemUIs.Add(item);
        }

        this.gameObject.SetActive(true);
    }

}
