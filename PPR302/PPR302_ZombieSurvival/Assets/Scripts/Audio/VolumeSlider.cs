using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeSlider : MonoBehaviour
{
    // these functions, upon hitting an apply button the volume of all sounds is reduced or inceased
    [SerializeField] private Slider volumeSlider = null;

    //[SerializeField] private TMPro volumeTextUI = null;



    public void VolumeSliderInput(float volume)
    {
        //changes volume as text
    }

    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}
