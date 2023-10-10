using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickUp;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickUp[id]);
        if (result == true)
            Debug.Log("Item Added");
        else
            Debug.Log("ITEM NOT ADDED");

    }

    public void GetSelectedItem()
    {
        Item recevedItem = inventoryManager.GetSelectedItem(false);
        if (recevedItem != null)
        {
            Debug.Log("Receved Item: " + recevedItem);
        }
        else
        {
            Debug.Log("No item receved");
        }
    }

    public void UseSelectedItem()
    {
        Item recevedItem = inventoryManager.GetSelectedItem(true);
        if (recevedItem != null)
        {
            Debug.Log("Receved Item: " + recevedItem);
        }
        else
        {
            Debug.Log("No item receved");
        }
    }
}
