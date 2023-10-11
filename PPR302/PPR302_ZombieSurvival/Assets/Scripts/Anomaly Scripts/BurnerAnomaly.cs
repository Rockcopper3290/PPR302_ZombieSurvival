using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnerAnomaly : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerStats playerStats;

    




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            playerStats.PlayerTakingDamage(5f);

        }
    }
}
