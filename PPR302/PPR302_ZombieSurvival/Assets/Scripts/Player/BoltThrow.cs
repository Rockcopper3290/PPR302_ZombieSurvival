using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltThrow : MonoBehaviour
{
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
    public KeyCode throwKey = KeyCode.G;
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
        if (Input.GetKeyDown(throwKey) && readyToThrow && itemCount > 0 && Time.timeScale != 0)
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

        // this sets the inventory manager as to allow the player the ability to pick up a thrown object
        projectile.GetComponent<DemoScript>().inventoryManager = inventoryManager;
        
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
