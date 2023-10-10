using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    [Header ("Player Refs")]
    public PlayerMovement playerMovement;
    public Slider staminaBar;
    //public Slider foodBar;
    //public Slider waterBar;

    [Header("Player stats")]
    public float maxHealth = 100f;

    [Header("Coroutine infomation")]
    private Coroutine Regen_Staminia;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);


    public float maxStamina = 100f;
    [HideInInspector] public float currentStamina;
    [HideInInspector] private float staminaUsagePerSecond = 2f;


    public float foodMeter = 100f;
    [HideInInspector] private float foodUsagePerSecond = 1f;

    public float waterMeter = 100f;
    [HideInInspector] private float waterUsagePerSecond = 1f;





    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    // 
    public void UseStamina(float amount)
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
