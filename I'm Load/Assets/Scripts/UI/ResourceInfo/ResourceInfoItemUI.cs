using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInfoItemUI : MonoBehaviour
{
    [SerializeField] Image Image_Icon;
    [SerializeField] Text Text_Count;

    public Item item;
    
    int prevCount;

    public void Update()
    {
        if(prevCount != item.count)
        {
            prevCount = item.count;
            Text_Count.text = Util.CountToString(item.count, 5);
        }
    }

    public void InitUI(Item item)
    {
        this.item = item;
        Image_Icon.sprite = this.item.db.iconSprite;

        prevCount = item.count;
        Text_Count.text = Util.CountToString(item.count, 5);
    }
}
