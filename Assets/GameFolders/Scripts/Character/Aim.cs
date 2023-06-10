using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using StarterAssets;
using UnityEngine.UI;


public class Aim : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] float AimSensitivity;
    [SerializeField] float NormalSensitivity;
    [SerializeField] RawImage CrossHair;
    [SerializeField] LayerMask AimRayLayerMask;
    [SerializeField] GameObject RayCastDebug;


    StarterAssetsInputs assetsInputs;
    ThirdPersonController thirdPerson;
    private void Start()
    {
        assetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPerson = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        if(assetsInputs.aim)
        {
            virtualCamera.gameObject.SetActive(true);
            thirdPerson.SetSensitivity(AimSensitivity);
            CrossHair.gameObject.SetActive(true);
            RayCastCrossHair();
        }
        else
        {
            virtualCamera.gameObject.SetActive(false);
            thirdPerson.SetSensitivity(NormalSensitivity);
            CrossHair.gameObject.SetActive(false);
        }
    }

    void RayCastCrossHair()
    {
        Vector2 ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, AimRayLayerMask))
        { RayCastDebug.transform.position = raycastHit.point; }
    }
}
