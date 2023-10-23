using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnerAnomaly : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerStats playerStats;

    private void OnTriggerEnter(Collider collidedAnomaly)
    {
        if (collidedAnomaly.gameObject.CompareTag("Burner Anomaly"))
        {
            Debug.Log("Hit the burner anomaly");

        }
    }

    private void OnTriggerStay(Collider collidedAnomaly)
    {
        if (collidedAnomaly.gameObject.CompareTag("Burner Anomaly"))
        {
            playerStats.PlayerTakingDamage(15f);

        }
    }
}
