using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickUp;
    
    // this chunk of code is redundent now -> replaced by Pickup.cs
    /*
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupItem(0);
                Destroy(this.gameObject);
            }
        }
    }
    */

    private void Start()
    {
        //inventoryManager = GameObject.Find("Inventory Manager").;
    }




    public void OnCollisionStay(Collision other)
    {
        //Debug.Log("Player in range");
        //Debug.Log(other.gameObject.name);

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(other.gameObject.name);

            PickupItem(0);
            Destroy(this.gameObject);
        }
    }

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
