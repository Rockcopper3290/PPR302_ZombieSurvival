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


    // TODO: change it so that it uses item when selected and pressing f
    public void UseSelectedItem()
    {
        Item recevedItem = inventoryManager.GetSelectedItem(true);

        //TODO: Check to see what tags the item has
        //TODO: If its a food item then add the food value to player stats and update
        //TODO: If its a water item then add the food value to player stats and update


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
