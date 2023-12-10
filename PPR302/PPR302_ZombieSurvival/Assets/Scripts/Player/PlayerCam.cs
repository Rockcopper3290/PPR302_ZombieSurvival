using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    [Header ("Camera Sensitivity")]
    [Range(200f, 600f)]
    public float sensX;
    [Range(200f, 600f)]
    public float sensY;

    [Header("Player References")]
    public Transform orientation;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    // =============================================================================================================

    // these functions, upon hitting an apply button the volume of all sounds is reduced or inceased
    [SerializeField] private Slider sensitivitySlider = null;

    //[SerializeField] private TMPro volumeTextUI = null;


    public void SaveAndApplySensButton()
    {
        // given as a decimal (0.4)
        float sensValue = sensitivitySlider.value;

        sensX = sensValue;
        sensY = sensValue;
    }
}
