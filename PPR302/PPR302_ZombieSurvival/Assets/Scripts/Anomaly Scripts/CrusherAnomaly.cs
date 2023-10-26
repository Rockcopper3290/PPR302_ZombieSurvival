using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherAnomaly : MonoBehaviour
{
    [Header("Refrences")]
    public GameObject playerObject;
    public PlayerMovement playerMovement;
    public PlayerStats playerStats;

    //[Header("Variables")]

    private void OnTriggerEnter(Collider collidedAnomaly)
    {
        if (collidedAnomaly.gameObject.CompareTag("Crusher Anomaly"))
        {
            Debug.Log("Hit the crusher anomaly");

            CalculateDistance();


        }
    }


    private void OnTriggerStay(Collider collidedAnomaly)
    {
        if (collidedAnomaly.gameObject.CompareTag("Burner Anomaly"))
        {
            playerStats.PlayerTakingDamage(20f);

        }
    }

    void CalculateDistance()
    {
        // Calculate distance between enemy and player
        //IMPORTANT
        //Vector3 distanceFromPlayer = Vector3.Distance(transform.position, playerObject.transform.position);
        //Debug.Log("Distance = " + distance);
    }

    private Vector3 CalculateDirection()
    {
        // Calculate direction from enemy to player in degrees
        Vector3 directionVector = (playerObject.transform.position - transform.position).normalized;
        float direction = Mathf.Atan2(directionVector.x, directionVector.z) * Mathf.Rad2Deg;
        return directionVector;
        //Debug.Log("Direction = " + direction);
    }

    void MovePlayerTowardAnomaly()
    {
        float tempMovespeed = 1f;
        // Move the enemy towards the player
        
        //IMPORTANT
        //playerObject.transform.Translate(CalculateDirection(), tempMovespeed * Time.deltaTime);

    }
}