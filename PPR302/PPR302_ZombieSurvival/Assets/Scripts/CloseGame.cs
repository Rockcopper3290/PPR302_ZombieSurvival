using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGame : MonoBehaviour
{
    void Update()
    {
        // Closes the game when player hits escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Game Was Closed");
            Application.Quit();
        }
    }
}
