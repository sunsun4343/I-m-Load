using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPopupUI_House : BuildingPopupUI
{
    [SerializeField] Text Text_BuildingbName;
    [SerializeField] BuildingPopupItemUI_HouseCitizen ItemPrefabs;
    [SerializeField] Transform ItemParent;

    List<BuildingPopupItemUI_HouseCitizen> ItemUIs = new List<BuildingPopupItemUI_HouseCitizen>();

    public void Show(Building_House building)
    {
        Text_BuildingbName.text = Str.Get(building.db.labelKey);

        for (int i = 0; i < ItemUIs.Count; i++)
        {
            Lean.Pool.LeanPool.Despawn(ItemUIs[i]);
        }
        ItemUIs.Clear();

        for (int i = 0; i < building.residenceCitizenComponent.residence_citizen.Count; i++)
        {
            var item = Lean.Pool.LeanPool.Spawn<BuildingPopupItemUI_HouseCitizen>(ItemPrefabs);
            item.transform.parent = ItemParent;
            item.gameObject.SetActive(true);
            item.InitUI(building.residenceCitizenComponent.residence_citizen[i]);
            ItemUIs.Add(item);
        }

        this.gameObject.SetActive(true);
    }
}
