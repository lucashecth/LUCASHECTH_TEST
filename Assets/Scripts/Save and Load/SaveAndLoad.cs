using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
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
