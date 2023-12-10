using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawn : MonoBehaviour
{

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
        
        //was used to find if an item was being spawned
        //Debug.Log(randomSelectedItem);
        if (items_LowIncome[randomSelectedItem].name == "Null")
        {
            // if the Item selected is null don't spawn anything
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
