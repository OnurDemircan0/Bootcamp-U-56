using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInVeinController : MonoBehaviour
{
    // Vir�s ya �ok b�y�d��� i�in patlayacak (�lecek)
    // Vir�s ya da �ok k���ld��� i�in patlayacak (�lecek)

    public bool hittedControl = false;
    private bool checkKillVirusControl = false;

    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject explosion2;

    private HitFeelingController hitFeelingController;

    private MutationControl mutationControl;

    private BossController bossController;

    private MutationControlForBrain mutationControlForBrain;

    private MedicineSwitching medicineSwitching;

    public float nowScale;
    public float wantedScale;
    [SerializeField] private float changeScaleValueEachHit;

    [SerializeField] private float minScaleForExplosion; // Bu de�ere ula��nca minimum k���lme de�erine ula�t��� i�in patlayacak
    [SerializeField] private float maxScaleForExplosion; // Bu de�ere ula��nca maximum b�y�me de�erine ula�t��� i�in patlayacak

    [SerializeField] private float changeSizeSpeed;

    [SerializeField] private float detectHitTime; // S�rekli Sald�r� oldu�u i�in belirli bir saniyede bir vurulmas� alg�mak i�in kullan�lacak 

    private bool detectControl;
    private bool explodedControl = false; //Patlama Efekti �al���p �al��mad���n� kontrol eder

    private bool medicineAndVirusSameColorControl; // Kullan�lan ila� ile vir�s ayn� renkte mi onu kontrol eder;

    private Color enemyColor;
    private Color medicineColor;

    private void Awake()
    {
        nowScale = transform.localScale.x;

        wantedScale = nowScale;

        detectControl = true;

        bossController = gameObject.GetComponent<BossController>();

        hitFeelingController = GameObject.Find("GameManager").gameObject.GetComponent<HitFeelingController>();
        medicineSwitching = GameObject.Find("GameManager").gameObject.GetComponent<MedicineSwitching>();

        mutationControl = gameObject.GetComponent<MutationControl>();

        mutationControlForBrain = gameObject.GetComponent<MutationControlForBrain>();

        Invoke("changeCheckKillVirusControl", 0.5f);

    }

    private void changeCheckKillVirusControl()
    {
        checkKillVirusControl = true;
    }


    public void enemyGetDamaged()
    {
        if (detectControl)
        {
            //Debug.Log("enemyGetDamaged �al��t�");

            //wantedScale = 0;

            wantedScale -= changeScaleValueEachHit; // Vir�s� K���lt�p patlatmak i�in

            if (wantedScale < 0)
            {
                wantedScale = 0;
            }

            //wantedScale += changeScaleValueEachHit; // Vir�s� b�y�t�p patlatmak i�in

            detectControl = false;

            Invoke("openDetectControl", detectHitTime);


            //enemyColor = mutationControl.virusMaterialColors[mutationControl.nowVirusMaterialColorsNumber];
            try
            {
                enemyColor = mutationControl.virusMaterialColors[mutationControl.targetVirusMaterialColorsNumber];
            }
            catch(Exception e)
            {
                try
                {
                    enemyColor = bossController.virusMaterialColors[bossController.targetVirusMaterialColorsNumber];
                }
                catch(Exception e2)
                {
                    enemyColor = mutationControlForBrain.virusMaterialNormalColors[mutationControlForBrain.targetVirusMaterialColorsNumber];
                }
                
            }

            //Debug.Log("enemyColor: " + enemyColor);

            try
            {
                medicineColor = mutationControl.virusMaterialColors[medicineSwitching.currentSelectedWeapon];
            }
            catch(Exception e)
            {
                try
                {
                    medicineColor = bossController.virusMaterialColors[medicineSwitching.currentSelectedWeapon];
                }
                catch (Exception e2)
                {
                    medicineColor = mutationControlForBrain.virusMaterialNormalColors[medicineSwitching.currentSelectedWeapon];
                }


            }

            //Debug.Log("medicineColor: " + medicineColor);

            medicineAndVirusSameColorControl = (medicineColor == enemyColor);

            //Debug.Log("medicineColor == enemyColor: " + medicineAndVirusSameColorControl.ToString());

            if (medicineAndVirusSameColorControl)
            {
                hitFeelingController.hittedVirus();

                try
                {
                    bossController.bossGetDamaged();
                }
                catch(Exception e)
                {

                }
            }
            else
            {
                try
                {
                    hitFeelingController.wrongMedicine(mutationControl);
                }
                catch(Exception e)
                {
                    try
                    {
                        mutationControlForBrain.mutateVirus();
                        //hitFeelingController.wrongMedicineForBrain(mutationControlForBrain);
                    }
                    catch (Exception e2)
                    {

                    }
                }


                try
                {
                    bossController.mutateBossVirus();
                }
                catch (Exception e)
                {

                }
            }
        }

    }

    private void openDetectControl()
    {
        detectControl = true;
    }


    private void Update()
    {
        if (hittedControl)
        {
            if(medicineAndVirusSameColorControl == true)
            {
                // K���lt�p Patlatmak ��in
                if (nowScale > wantedScale)
                {
                    transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f) * changeSizeSpeed * Time.deltaTime;   // K���lt�p Patlatmak ��in

                    nowScale = transform.localScale.x;

                }
                else
                {
                    hittedControl = false;
                }


                // B�y�lt�p Patlatmak ��in
                /*
                else if (nowScale < wantedScale)
                {
                    transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * changeSizeSpeed * Time.deltaTime; // B�y�lt�p Patlatmak ��in

                    nowScale = transform.localScale.x;
                }
                */
            }

        }

        //Debug.Log("nowScale: " + nowScale.ToString());

        if ((nowScale >= maxScaleForExplosion || nowScale <= minScaleForExplosion) && checkKillVirusControl)
        {
            Debug.Log("Enemey patlad� (�ld�)");

            

            if (explodedControl == false)
            {
                Instantiate(explosion2, transform.position, Quaternion.identity);
                //explosion2.SetActive(true);
                gameObject.transform.localScale = new Vector3(0, 0, 0);

                explodedControl = true;

                hitFeelingController.killedVirus();
            }


            Destroy(gameObject,2);


        }
    }
}
