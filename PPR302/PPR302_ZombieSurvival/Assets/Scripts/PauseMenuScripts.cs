using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScripts : MonoBehaviour
{
    public GameObject MainInventory;
    public GameObject ToolBarUI;
    public GameObject Crosshair;
    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject BloodOverlay;
    public Image BloodOverlay_Image;

    public PlayerStats playerStats;

    // run code once
    private void Awake()
    {
        if (SceneManager.sceneCount == 0)
        {
            //Do nothing as you are in the main start menu
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //BloodOverlay_Image = GetComponent<Image>();
    }

    private void Update()
    {
        //if (playerStats.currentHealth < 100)
        //{
        //    BloodOverlay.SetActive(true);
        //}
        //else
        //{
        //    BloodOverlay.SetActive(false);
        //}


        if (SceneManager.sceneCount == 0)
        {
            //Do nothing as you are in the main start menu
        }
        else
        {

            // if inventory is not up
            if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenuUI.activeInHierarchy && !OptionsMenuUI.activeInHierarchy)
            {
                // freeze time
                Time.timeScale = 0;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                MainInventory.SetActive(false);
                ToolBarUI.SetActive(false);
                Crosshair.SetActive(false);
                PauseMenuUI.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && PauseMenuUI.activeInHierarchy)
            {
                // resume time
                Time.timeScale = 1;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                MainInventory.SetActive(false);
                ToolBarUI.SetActive(true);
                Crosshair.SetActive(true);
                PauseMenuUI.SetActive(false);


            }

            if (playerStats.currentHealth <= 0 && SceneManager.sceneCount == 1)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                SceneManager.LoadScene("You Died");
            }
        }

    }

    public void play_Button()
    {
        SceneManager.LoadScene("Main City Scene");
    }


    public void MainMenu_Button()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Game Scene Stuff
    public void return_Button()
    {
        // resume time
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        MainInventory.SetActive(false);
        ToolBarUI.SetActive(true);
        Crosshair.SetActive(true);
        PauseMenuUI.SetActive(false);
    }

    public void optionsMenu_Button()
    {
        // Will open up the options menu when implimented
    }

    public void quitToMenu_Button()
    {
        //Load the main Menu Screne
        SceneManager.LoadScene("Main Menu");
    }

    public void quitToDesktop_Button()
    {
        Debug.Log("Game Was Closed");
        Application.Quit();
    }
}
