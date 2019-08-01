using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInfoUI : MonoBehaviour
{
    [SerializeField] GamePlayData gamePlayData;
    [SerializeField] ResourceInfoItemUI ItemPrefabs;
    [SerializeField] Transform ItemParent;

    List<ResourceInfoItemUI> ItemUIs = new List<ResourceInfoItemUI>();

    public void Start()
    {
        //Default
        Create_ResourceInfoItem(ItemDB.Type.Wood);
        Create_ResourceInfoItem(ItemDB.Type.Stone);
    }

    void Create_ResourceInfoItem(ItemDB.Type type)
    {
        var item = Lean.Pool.LeanPool.Spawn<ResourceInfoItemUI>(ItemPrefabs);
        item.transform.parent = ItemParent;
        item.gameObject.SetActive(true);
        item.InitUI(gamePlayData.GetItemSurely(type));
        ItemUIs.Add(item);
    }



}
