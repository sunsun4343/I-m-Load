using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingComponent_StorageItem
{
    public Building building;
    public List<Item> storage_items = new List<Item>();
    

    public BuildingComponent_StorageItem(Building building)
    {
        this.building = building;
    }

    public void StoreItem(Item item)
    {
        bool isExist = false;
        for (int i = 0; i < storage_items.Count; i++)
        {
            if (storage_items[i].db.type == item.db.type)
            {
                isExist = true;
                storage_items[i].count += item.count;
                break;
            }
        }

        if (isExist == false)
        {
            storage_items.Add(item);
        }
    }

    public Item PullItem(ItemDB.Type type, uint count)
    {
        for (int i = 0; i < storage_items.Count; i++)
        {
            if (storage_items[i].db.type == type)
            {
                if (count >= storage_items[i].count)
                {
                    Item item = storage_items[i];
                    storage_items.RemoveAt(i);
                    return item;
                }
                else if (count < storage_items[i].count)
                {
                    Item item = new Item();
                    item.db = storage_items[i].db;
                    item.count = (int)count;
                    storage_items[i].count -= (int)count;
                    return item;
                }
            }
        }
        return null;
    }

    public Item[] PullItem(ItemDB.Category category, uint count)
    {
        List<Item> items = new List<Item>();

        for (int i = 0; i < storage_items.Count; i++)
        {
            if (storage_items[i].db.category == category)
            {
                if (count == storage_items[i].count)
                {
                    items.Add(storage_items[i]);
                    storage_items.RemoveAt(i);
                    return items.ToArray();
                }
                else if(count > storage_items[i].count)
                {
                    count -= (uint)storage_items[i].count;
                    items.Add(storage_items[i]);
                    storage_items.RemoveAt(i);
                }
                else if (count < storage_items[i].count)
                {
                    Item item = new Item();
                    item.db = storage_items[i].db;
                    item.count = (int)count;
                    items.Add(item);
                    storage_items[i].count -= (int)count;
                    return items.ToArray();
                }
            }
        }
        return items.ToArray();
    }


}
