using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this anomaly script is used for the whirlpool/black hole anomalies.
// I call it the crusher anomaly cause it causes a crushing effect on the player
public class CrusherAnomaly : MonoBehaviour
{


    [Header("Variables")]
    public float rotateSpeed;

    void Update()
    {
        //this will deturmin the speed in which the anomaly rotates
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    public PlayerStats playerStats;

    private void OnTriggerEnter(Collider collidedItem)
    {
        // If an item enters an anomaly it'll destory the item
        if (collidedItem.gameObject.CompareTag("Is Item"))
        {
            Destroy(collidedItem.gameObject);
        }
    }

    private void OnTriggerStay(Collider collidedAnomaly)
    {
        if (collidedAnomaly.gameObject.CompareTag("Player"))
        {
            playerStats.PlayerTakingDamage(25f);

        }
    }

}