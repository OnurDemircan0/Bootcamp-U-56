using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Aim : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public bool aim;

    public void OnAim(InputValue value)
    {
        AimInput(value.isPressed);  //This function checks if the Aim Value is pressed and sends that result to the AimInput Function
        Debug.Log("Pressed");
    }
    
    public void AimInput(bool newAimState) //This Function sets the aim bool above, to the new AimState we have give
    {
        aim = newAimState; 
    }

    private void Update()
    {
        if(aim)
        {
            virtualCamera.gameObject.SetActive(true);
        }
        else
        {
            virtualCamera.gameObject.SetActive(false);
        }
    }
}
