using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltThrow : MonoBehaviour
{

    //TODO: Get refrences to what the player has selected


    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public InventoryManager inventoryManager;
    public Item currentItem;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    public void Update()
    {
        int itemCount = inventoryManager.GetStackAmount();
        if (Input.GetKeyDown(throwKey) && readyToThrow && itemCount > 0)
        {
            //gets current item
            currentItem = inventoryManager.GetSelectedItem(false);
            objectToThrow = currentItem.prefabedItem;
            if (objectToThrow != null)
            {
                Throw();
            }
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        //instantate object to throw
        
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);
        //projectile.GetComponent<InventoryManager>() = inventoryManager;

        //get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        inventoryManager.GetSelectedItem(true);

        objectToThrow = null;
        currentItem = null;

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }


}
