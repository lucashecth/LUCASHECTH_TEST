using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Serializable]
    public class ItemData
    {
        public string itemID;
        public int count;
        public int slotIndex;
    }

    [System.Serializable]
    public class InventoryData
    {
        public List<ItemData> items = new List<ItemData>();
    }

}
