using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnerAnomaly : MonoBehaviour
{
    public PlayerStats playerStats;

    private void OnTriggerEnter(Collider collidedItem)
    {
        // If an item enters an anomaly it'll destory the item
        if (collidedItem.gameObject.CompareTag("Is Item"))
        {
            Destroy(collidedItem.gameObject);
        }
    }

    private void OnTriggerStay(Collider collidedWithAnomaly)
    {
        if (collidedWithAnomaly.gameObject.CompareTag("Player"))
        {
            playerStats.PlayerTakingDamage(30f);

        }
    }
}
