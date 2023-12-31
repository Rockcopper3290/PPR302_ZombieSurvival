using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private GameObject pickUpUI;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    private RaycastHit hit;

    public GameObject itemOnGround;
    public DemoScript demoScript;
    public InventoryManager inventoryManager;


    // Every frame shoot out a raycast
    void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);

        // Resets old raycast
        if (hit.collider != null)
        {
            //hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);
        }

        // if the raycast collides with a pickable object
        if (Physics.Raycast(
            playerCameraTransform.position, 
            playerCameraTransform.forward, 
            out hit, 
            hitRange, 
            pickableLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickUpUI.SetActive(true);

            Debug.Log("F was pressed");

            //get info of the game object
            demoScript = hit.collider.gameObject.GetComponent<DemoScript>();
            itemOnGround = hit.collider.gameObject;

            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(demoScript);
                Debug.Log(itemOnGround);


                Debug.Log("F was pressed");
                demoScript.PickupItem(0);
                Destroy(itemOnGround);
                ResetRaycast();
            }
            // END IF
        }
    }

    public void ResetRaycast()
    {
        //hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
        pickUpUI.SetActive(false);

        demoScript = null;
        itemOnGround = null;
    }
}
