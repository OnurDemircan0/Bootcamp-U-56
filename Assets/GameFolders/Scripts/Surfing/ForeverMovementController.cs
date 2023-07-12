using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class ForeverMovementController : MonoBehaviour
{
    [SerializeField] private float startPoint;
    [SerializeField] private float finishPoint;

    private SurfingControllerV2 surfingControllerV2;
    private VirusController virusController;
    private OtherAlyuvarController otherAlyuvarController;


    [SerializeField] private CinemachineVirtualCamera playerFollowCameraVariant;

    private Vector3 playerFollowCameraVariantFirstDamping;


    void Awake()
    {
        surfingControllerV2 = null;
        virusController = null;
        otherAlyuvarController = null;

        try
        {
            surfingControllerV2 = gameObject.GetComponent<SurfingControllerV2>();
        }
        catch(Exception e)
        {
            try
            {
                virusController = gameObject.GetComponent<VirusController>();
            }
            catch (Exception e2)
            {
                otherAlyuvarController = gameObject.GetComponent<OtherAlyuvarController>();
            }
        }



        playerFollowCameraVariantFirstDamping = playerFollowCameraVariant.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping;

        Debug.Log("playerFollowCameraVariantFirstDamping: " + playerFollowCameraVariantFirstDamping.ToString());

        playerFollowCameraVariant.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping = new Vector3(0, 0, 0);



    }



    void Update()
    {
        if(surfingControllerV2 != null)
        {
            if(playerFollowCameraVariant.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping != playerFollowCameraVariantFirstDamping)
            {
                playerFollowCameraVariant.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping = playerFollowCameraVariantFirstDamping ;
            }

            if(surfingControllerV2.dstTravelled >= finishPoint)
            {
                surfingControllerV2.dstTravelled = startPoint;

                playerFollowCameraVariant.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping = new Vector3(0, 0, 0);

                //Invoke("turnNormalDampingValue", 0.1f);
            }
        }
    }
}
