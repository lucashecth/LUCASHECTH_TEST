using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> items;

    private Dictionary<int, Item> itemLookup;

    public void Initialize()
    {
        itemLookup = new Dictionary<int, Item>();
        foreach (Item item in items)
        {
            if (!itemLookup.ContainsKey(item.id))
            {
                itemLookup.Add(item.id, item);
            }
        }
    }

    public Item GetItemByID(int id)
    {
        if (itemLookup == null) Initialize();
        itemLookup.TryGetValue(id, out Item item);
        return item;
    }
}
