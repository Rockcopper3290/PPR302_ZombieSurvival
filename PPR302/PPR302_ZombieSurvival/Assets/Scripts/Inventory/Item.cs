using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu (menuName = "Scriptable object/Item")]
public class Item : ScriptableObject {

    //public TileBase tile;
    
    


    [Header("Only gameplay")]
    public ItemType type;
    public ActionType actionType;
    
    // how much of a givin value is restored by the given item
    public float restoreValue;

    [Header("Only UI")]
    public bool stackable = true;
    public int amountOfItemsPerStack = 1;

    [Header("Both")]
    public Sprite image;

}

public enum ItemType
{
    Food,
    Water,
    Tool,
    Weapon,
    Misc
}

public enum ActionType
{
    Consumable,
    NONE
}

