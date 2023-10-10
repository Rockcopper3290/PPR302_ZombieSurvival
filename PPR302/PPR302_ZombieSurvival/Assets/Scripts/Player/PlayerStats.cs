using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    [Header ("Player Refs")]
    public PlayerMovement playerMovement;
    public Slider healthBar;
    public Slider staminaBar;
    public Slider foodBar;
    public Slider waterBar;

    [Header("Player stats")]
    public float maxHealth = 100f;
    [HideInInspector] public float currentHealth;
    [HideInInspector] private float HealthUsagePerSecond_Starving_Or_Dehydrated = 2f;
    [HideInInspector] private float HealthUsagePerSecond_Starving_AND_Dehydrated = 5f;

    public float maxStamina = 100f;
    [HideInInspector] public float currentStamina;
    [HideInInspector] private float staminaUsagePerSecond = 2f;
    


    public float maxFood = 100f;
    [HideInInspector] public float currentFood;
    [HideInInspector] private float foodUsagePerSecond = 1f;

    public float maxWater = 100f;
    [HideInInspector] public float currentWater;
    [HideInInspector] private float waterUsagePerSecond = 1f;

    [Header("Coroutine infomation")]
    private Coroutine Regen_Staminia;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);



    // Set Max & currents of all the player stats
    void Start()
    {

        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;

        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;

        currentFood = maxFood;
        foodBar.maxValue = maxFood;
        foodBar.value = maxFood;

        currentWater = maxWater;
        waterBar.maxValue = maxWater;
        waterBar.value = maxWater;

        //SetSliderValues(this.currentStamina, this.maxStamina, staminaBar);
    }

    public void SetSliderValues(float currentValue, float maxValue, Slider targetSlider)
    {
        currentValue = maxValue;
        targetSlider.maxValue = maxValue;
        targetSlider.value = maxValue;
        //return (currentValue, maxValue, targetSlider);
    }


    // 
    public void UseStamina_Jumping(float amount)
    {
        if (currentStamina - amount >= 0)
        {
            
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            // if not null it means that were are already regaining staminia -> stop the current staminia regen and wait 2 seconds
            if (Regen_Staminia != null)
                StopCoroutine(Regen_Staminia);

            Regen_Staminia = StartCoroutine(RegainStaminia());
        }
        else
        {
            Debug.Log("Not Enough stamina");
        }
    }

    public void UseStamina_Sprinting(float amount)
    {
        if (currentStamina - amount >= 0)
        {

            currentStamina -= amount * Time.deltaTime;
            staminaBar.value = currentStamina;

            // if not null it means that were are already regaining staminia -> stop the current staminia regen and wait 2 seconds
            if (Regen_Staminia != null)
                StopCoroutine(Regen_Staminia);

            Regen_Staminia = StartCoroutine(RegainStaminia());
        }
        else
        {
            Debug.Log("Not Enough stamina");
        }
    }


    // Coroutines --------------------------------------------------------------------------------------
    private IEnumerator RegainStaminia()
    {
        yield return new WaitForSeconds(2f);

        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        Regen_Staminia = null;
    }

}
