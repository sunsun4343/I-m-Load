using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPopupItemUI_StorageItem : BuildingPopupItemUI
{
    [SerializeField] Image Image_icon;
    [SerializeField] Text Text_Name;
    [SerializeField] Text Text_Count;

    public void InitUI(Item item)
    {
        Image_icon.sprite = item.db.iconSprite;
        Text_Name.text = item.db.label;
        Text_Count.text = item.count.ToString();
    }

}
