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
    public Coroutine HealthOverTime_CR;
    [HideInInspector] public float currentHealth;
    //[HideInInspector] private float HealthUsagePerSecond_Starving_Or_Dehydrated = 2f;
    //[HideInInspector] private float HealthUsagePerSecond_Starving_AND_Dehydrated = 5f;

    public float maxStamina = 100f;
    [HideInInspector] public float currentStamina;
    //[HideInInspector] private float staminaUsagePerSecond = 2f;
    


    public float maxFood = 100f;
    private Coroutine LoseFoodOverTime_CR;
    [HideInInspector] public float currentFood;
    [HideInInspector] private float foodUsagePerSecond = 2f;

    public float maxWater = 100f;
    private Coroutine LoseWaterOverTime_CR;
    [HideInInspector] public float currentWater;
    [HideInInspector] private float waterUsagePerSecond = 2f;

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

        //HealthOverTime_CR = StartCoroutine(HealthOverTime());
        //LoseFoodOverTime_CR = StartCoroutine(LoseFoodOverTime());
        //LoseWaterOverTime_CR = StartCoroutine(LoseWaterOverTime());

        StartCoroutine(HealthOverTime());
        StartCoroutine(LoseFoodOverTime());
        StartCoroutine(LoseWaterOverTime());


    }

    public void SetSliderValues(float currentValue, float maxValue, Slider targetSlider)
    {
        currentValue = maxValue;
        targetSlider.maxValue = maxValue;
        targetSlider.value = maxValue;
        //return (currentValue, maxValue, targetSlider);
    }

    public void AddToPlayerStats_Food(float restoreAmount)
    {
        

        currentFood += restoreAmount;

        if (currentFood > maxFood)
            currentFood = maxFood;

        foodBar.value = currentFood;

        // if not null it means that were are already regaining staminia -> stop the current staminia regen and wait 2 seconds
        //StopCoroutine(LoseFoodOverTime());

        //LoseFoodOverTime_CR = StartCoroutine(LoseFoodOverTime());
    }

    public void AddToPlayerStats_Water(float restoreAmount)
    {
        currentWater += restoreAmount;

        if (currentWater > 100f)
            currentWater = 100f;

        foodBar.value = currentFood;

        // if not null it means that were are already regaining staminia -> stop the current staminia regen and wait 2 seconds
        //StopCoroutine(LoseWaterOverTime());

        //LoseWaterOverTime_CR = StartCoroutine(LoseWaterOverTime());
    }

    public void AddToPlayerStats_Heath(float restoreAmount)
    {
        currentHealth += restoreAmount;

        if (currentHealth > 100f)
            currentHealth = 100f;

        healthBar.value = currentHealth;

        
    }



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
            { 
            StopCoroutine(Regen_Staminia);
            }

            Regen_Staminia = StartCoroutine(RegainStaminia());
        }
        else
        {
            Debug.Log("Not Enough stamina");
        }
    }

    public void PlayerTakingDamage(float damage)
    {
        


        currentHealth -= damage * Time.deltaTime;
        healthBar.value = currentHealth;
        
        if (HealthOverTime_CR != null)
        {
            StopCoroutine(HealthOverTime_CR);
            Debug.Log("alls good");
        }
        HealthOverTime_CR = StartCoroutine(HealthOverTime());

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

    private IEnumerator LoseFoodOverTime()
    {
        yield return new WaitForSeconds(2f);

        //while player is walking and has food stores
        while (currentFood > 0f) //&& playerMovement.state == PlayerMovement.MovementState.walking
        {
            
            currentFood -= foodUsagePerSecond * Time.deltaTime;
            foodBar.value = currentFood;
            yield return regenTick;
        }
        LoseFoodOverTime_CR = null;
    }

    private IEnumerator LoseWaterOverTime()
    {
        yield return new WaitForSeconds(2f);

        //while player is walking and has food stores
        while (currentWater > 0f)
        {

            currentWater -= waterUsagePerSecond * Time.deltaTime;
            waterBar.value = currentWater;
            yield return regenTick;
        }
        LoseWaterOverTime_CR = null;
    }

    private IEnumerator HealthOverTime()
    {
        yield return new WaitForSeconds(2f);

        //while player is walking and has food stores
        while (currentFood > 0f && currentWater > 0f) // if the player is fine on stats regain health slowly
        {
            while (currentFood < 30f || currentWater < 30f) // if the player has one Stat below 30%, Reduce health by a small amount
            {
                while (currentFood < 30f && currentWater < 30f) //If both stats are below 30%, Reduce health by a large amount
                {
                    currentHealth -= 8f * Time.deltaTime;
                    healthBar.value = currentHealth;
                    yield return regenTick;
                }

                currentHealth -= 4f * Time.deltaTime;
                healthBar.value = currentHealth;
                yield return regenTick;
            }

            if (currentHealth <100f && currentFood > 50f && currentWater > 50f)
            {
                currentHealth += 10f * Time.deltaTime;
                if (currentHealth >= 100f)
                {
                    currentHealth = 100f;
                }
                healthBar.value = currentHealth;
                yield return regenTick;
            }

            yield return regenTick;
        }
        HealthOverTime_CR = null;
    }

}
