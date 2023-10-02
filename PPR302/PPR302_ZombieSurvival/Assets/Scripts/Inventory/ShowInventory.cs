using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInventory : MonoBehaviour
{
    public GameObject MainInventory;
    void Update()
    {
        // if inventory is not up
        if (Input.GetKeyDown(KeyCode.E) && !MainInventory.activeInHierarchy)
        {
            // freeze time
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            MainInventory.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.E) && MainInventory.activeInHierarchy)
        {
            // resume time
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            MainInventory.SetActive(false);
        }
    }
}
