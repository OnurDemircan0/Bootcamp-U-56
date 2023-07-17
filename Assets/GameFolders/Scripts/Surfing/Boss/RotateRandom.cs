using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRandom : MonoBehaviour
{
    [SerializeField] private SurfingControllerV2 surfingControllerV2;
    [SerializeField] private BossController bossController;

    [SerializeField] private bool forBossVirusControl = true;

    //[SerializeField] private float rotationSpeed;
    [SerializeField] private float changeRotationLerpTime;

    [SerializeField] private float maxRotationAngleBeforeCompleteGrow;
    [SerializeField] private float maxRotationAngleAfterCompleteGrow;

    [SerializeField] private float maxRotationAngleForNormalObjects;

    private bool determineRandomRotationValuesForBeforeCompleteGrowControl = false;
    private bool determineRandomRotationValuesForAfterCompleteGrowControl = false;
    

    private float randomRotationX = 0;
    private float randomRotationY = 0;
    private float randomRotationZ = 0;


    private float randomRotationNewX = 0;
    private float randomRotationNewY = 0;
    private float randomRotationNewZ = 0;

    private void Start()
    {
        //determineRandomRotationValuesForBeforeCompleteGrow();

        if(forBossVirusControl == false)
        {
            determineRandomRotationValuesForNormalObjects();
        }
    }

    void Update()
    {
        transform.Rotate(randomRotationX, randomRotationY, randomRotationZ, Space.Self);

        /*
        transform.Rotate(randomRotationX * rotationSpeed * Time.deltaTime,
            randomRotationY * rotationSpeed * Time.deltaTime,
            randomRotationZ * rotationSpeed * Time.deltaTime,
            Space.Self);
        */

        if (forBossVirusControl)
        {
            if(surfingControllerV2 != null)
            {
                if (surfingControllerV2.contactBossControl)
                {


                    if (determineRandomRotationValuesForBeforeCompleteGrowControl == false)
                    {
                        determineRandomRotationValuesForBeforeCompleteGrow();
                        determineRandomRotationValuesForBeforeCompleteGrowControl = true;


                    }

                    if (bossController.reachMaxSize)
                    {
                        if (determineRandomRotationValuesForAfterCompleteGrowControl == false)
                        {
                            determineRandomRotationValuesForAfterCompleteGrow();
                            determineRandomRotationValuesForAfterCompleteGrowControl = true;
                        }
                    }
                }
            }
            
        }
        

    }

    private void determineRandomRotationValuesForBeforeCompleteGrow()
    {
        randomRotationNewX = Random.Range(maxRotationAngleBeforeCompleteGrow * -1, maxRotationAngleBeforeCompleteGrow);
        randomRotationNewY = Random.Range(maxRotationAngleBeforeCompleteGrow * -1, maxRotationAngleBeforeCompleteGrow);
        randomRotationNewZ = Random.Range(maxRotationAngleBeforeCompleteGrow * -1, maxRotationAngleBeforeCompleteGrow);

        StartCoroutine(changeRotateSpeed());
    }


    private void determineRandomRotationValuesForAfterCompleteGrow()
    {
        randomRotationNewX = Random.Range(maxRotationAngleAfterCompleteGrow * -1, maxRotationAngleAfterCompleteGrow);
        randomRotationNewY = Random.Range(maxRotationAngleAfterCompleteGrow * -1, maxRotationAngleAfterCompleteGrow);
        randomRotationNewZ = Random.Range(maxRotationAngleAfterCompleteGrow * -1, maxRotationAngleAfterCompleteGrow);

        StartCoroutine(changeRotateSpeed());
    }

    private void determineRandomRotationValuesForNormalObjects()
    {
        randomRotationX = Random.Range(maxRotationAngleForNormalObjects * -1, maxRotationAngleForNormalObjects);
        randomRotationY = Random.Range(maxRotationAngleForNormalObjects * -1, maxRotationAngleForNormalObjects);
        randomRotationZ = Random.Range(maxRotationAngleForNormalObjects * -1, maxRotationAngleForNormalObjects);

        //StartCoroutine(changeRotateSpeed());
    }


    IEnumerator changeRotateSpeed()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            randomRotationX = Mathf.Lerp(randomRotationX, randomRotationNewX, changeRotationLerpTime);
            randomRotationY = Mathf.Lerp(randomRotationY, randomRotationNewY, changeRotationLerpTime);
            randomRotationZ = Mathf.Lerp(randomRotationZ, randomRotationNewZ, changeRotationLerpTime);

            //Debug.Log("randomRotationX: " + randomRotationX);
            //Debug.Log("randomRotationY: " + randomRotationY);
            //Debug.Log("randomRotationZ: " + randomRotationZ);

        }
    }
}
