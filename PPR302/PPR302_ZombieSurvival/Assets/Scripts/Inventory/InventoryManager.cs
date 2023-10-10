using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    

    int selectedSlot = -1;

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        //get the input of a checked key
        if (Input.inputString != null){
            //check if key is a number
            bool isNumber = int.TryParse(Input.inputString, out int number);
            // check if number is between our range
            if (isNumber && number > 0 && number < 8){
                ChangeSelectedSlot(number - 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseSelectedItem();
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        // check if any slot has the same item count with coult lower then max
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInSlot != null && 
                itemInSlot.item == item &&
                itemInSlot.item.stackable == true &&
                itemInSlot.count < item.amountOfItemsPerStack)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++){
            InventorySlot slot = inventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
            if (itemInSlot == null){
                // if empty slot spawn item here
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        DraggableItem inventoryItem = newItemGo.GetComponent<DraggableItem>();
        inventoryItem.InitialiseItem(item); 
    }


    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
        if (itemInSlot != null){
            Item item = itemInSlot.item;
            if (use)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }

            return item;
        }

        return null;
    }

    public Item UseSelectedItem()
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;

            //checks to see if the current selected item is consumeable
            if (item.actionType == ActionType.Consumable)
            {
                Debug.Log("Item is consumeable");
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                    Debug.Log("USED LAST OF ITEM");
                }
                else
                {
                    itemInSlot.RefreshCount();
                    Debug.Log("USED ITEM");
                }
            }
            else
            {
                Debug.Log("Item in slot is not consumeable");
            }



        }
        else
        {
            Debug.Log("Nothing was found in slot");
        }


        return null;
    }
}
