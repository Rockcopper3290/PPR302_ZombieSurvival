using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this anomaly script is used for the whirlpool/black hole anomalies.
// I call it the crusher anomaly cause it causes a crushing effect on the player
public class CrusherAnomaly : MonoBehaviour
{


    [Header("Variables")]
    public float rotateSpeed;

    [Header("Damage overlay")]
    public Image damageOverlay;

    private void Start()
    {
    }

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
            FindObjectOfType<AudioManager>().PlayAudioClip("Object.Poof");

        }
        else if(collidedItem.gameObject.CompareTag("Player"))
        {
            //damageOverlay = GetComponent<Image>();
            Color tempColor = damageOverlay.color;
            tempColor = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, 100f);
            damageOverlay.color = tempColor;
            FindObjectOfType<AudioManager>().PlayAudioClip("Blood splatter 1");
        }


    }

    private void OnTriggerStay(Collider collidedAnomaly)
    {
        if (collidedAnomaly.gameObject.CompareTag("Player"))
        {
            playerStats.PlayerTakingDamage(25f);

        }
    }

    private void OnTriggerExit(Collider collided)
    {
        if (collided.gameObject.CompareTag("Player"))
        {
            //damageOverlay = GetComponent<Image>();
            Color tempColor = damageOverlay.color;
            tempColor = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, 0f);
            damageOverlay.color = tempColor;
        }
    }

}