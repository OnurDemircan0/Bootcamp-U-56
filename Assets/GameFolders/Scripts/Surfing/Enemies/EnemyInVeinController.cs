using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInVeinController : MonoBehaviour
{
    // Virüs ya çok büyüdüðü için patlayacak (Ölecek)
    // Virüs ya da çok küçüldüðü için patlayacak (Ölecek)

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

    [SerializeField] private float minScaleForExplosion; // Bu deðere ulaþýnca minimum küçülme deðerine ulaþtýðý için patlayacak
    [SerializeField] private float maxScaleForExplosion; // Bu deðere ulaþýnca maximum büyüme deðerine ulaþtýðý için patlayacak

    [SerializeField] private float changeSizeSpeed;

    [SerializeField] private float detectHitTime; // Sürekli Saldýrý olduðu için belirli bir saniyede bir vurulmasý algýmak için kullanýlacak 

    private bool detectControl;
    private bool explodedControl = false; //Patlama Efekti çalýþýp çalýþmadýðýný kontrol eder

    private bool medicineAndVirusSameColorControl; // Kullanýlan ilaç ile virüs ayný renkte mi onu kontrol eder;

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
            //Debug.Log("enemyGetDamaged çalýþtý");

            //wantedScale = 0;

            wantedScale -= changeScaleValueEachHit; // Virüsü Küçültüp patlatmak için

            if (wantedScale < 0)
            {
                wantedScale = 0;
            }

            //wantedScale += changeScaleValueEachHit; // Virüsü büyütüp patlatmak için

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
                // Küçültüp Patlatmak Ýçin
                if (nowScale > wantedScale)
                {
                    transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f) * changeSizeSpeed * Time.deltaTime;   // Küçültüp Patlatmak Ýçin

                    nowScale = transform.localScale.x;

                }
                else
                {
                    hittedControl = false;
                }


                // Büyültüp Patlatmak Ýçin
                /*
                else if (nowScale < wantedScale)
                {
                    transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * changeSizeSpeed * Time.deltaTime; // Büyültüp Patlatmak Ýçin

                    nowScale = transform.localScale.x;
                }
                */
            }

        }

        //Debug.Log("nowScale: " + nowScale.ToString());

        if ((nowScale >= maxScaleForExplosion || nowScale <= minScaleForExplosion) && checkKillVirusControl)
        {
            Debug.Log("Enemey patladý (Öldü)");

            

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
