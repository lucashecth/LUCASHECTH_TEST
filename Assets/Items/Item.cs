using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public int id;
    public TileBase tileBase;
    public Sprite image;
    public ItemType itemType;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);
    public bool stackable = true;
    public enum ItemType
    {
        HealthItem,
        Weapon,
        Tool
    }
    public enum ActionType
    {
        Oxygen,
        Water,
        Equip
    }

}
