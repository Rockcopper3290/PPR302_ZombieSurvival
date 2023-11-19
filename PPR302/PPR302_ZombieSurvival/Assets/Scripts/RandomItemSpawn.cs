using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawn : MonoBehaviour
{

    //TODO Spawning: have a list of all spawnable object objects
    //TODO Spawning LOW-Pri: Make a list of spawnables for each type of area (Res, shops and indust)
    //TODO Spawning: Assign each item with a value (0f -> 1f) for the likelyhood of that item spawning
    //TODO Spawning: on start, roll the chance and instancate that item.

    public GameObject SpawnLocation_Obj;

    public List<GameObject> listOfGameItems = new List<GameObject>();
    public List<GameObject> items_LowIncome = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Will randomly spawn an Item from the LowIncome List
        Instantate_LowIncomeRegionItem();
    }

    void Instantate_LowIncomeRegionItem()
    {
        int randomSelectedItem = Random.Range(0, items_LowIncome.Count);
        Debug.Log(randomSelectedItem);
        if (items_LowIncome[randomSelectedItem].name == "Null")
        {
            // The first Item of each list will be a null object
        }
        else
        {
            Vector3 spawnLocation = new Vector3(SpawnLocation_Obj.transform.position.x, 
                                                SpawnLocation_Obj.transform.position.y, 
                                                SpawnLocation_Obj.transform.position.z);
            GameObject SpawnedItem = items_LowIncome[randomSelectedItem];
            Instantiate(SpawnedItem, spawnLocation, transform.rotation);
        }

            


    }

}
