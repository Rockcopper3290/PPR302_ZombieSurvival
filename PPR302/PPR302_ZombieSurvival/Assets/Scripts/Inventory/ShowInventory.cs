using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInventory : MonoBehaviour
{
    public GameObject MainInventory;
    public GameObject Crosshair;
    void Update()
    {
        // if inventory is not up
        if (Input.GetKeyDown(KeyCode.I) && !MainInventory.activeInHierarchy)
        {
            // freeze time
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            MainInventory.SetActive(true);
            Crosshair.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.I) && MainInventory.activeInHierarchy)
        {
            // resume time
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            MainInventory.SetActive(false);
            Crosshair.SetActive(true);

        }
    }
}
