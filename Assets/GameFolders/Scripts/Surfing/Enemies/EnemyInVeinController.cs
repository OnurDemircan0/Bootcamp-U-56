using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInVeinController : MonoBehaviour
{
    // Virüs ya çok büyüdüðü için patlayacak (Ölecek)
    // Virüs ya da çok küçüldüðü için patlayacak (Ölecek)

    public bool hittedControl = false;

    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject explosion2;

    private HitFeelingController hitFeelingController;

    private MutationControl mutationControl;

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

        hitFeelingController = GameObject.Find("GameManager").gameObject.GetComponent<HitFeelingController>();
        medicineSwitching = GameObject.Find("GameManager").gameObject.GetComponent<MedicineSwitching>();

        mutationControl = gameObject.GetComponent<MutationControl>();

    }


    public void enemyGetDamaged()
    {
        if (detectControl)
        {
            //Debug.Log("enemyGetDamaged çalýþtý");

            wantedScale -= changeScaleValueEachHit; // Virüsü Küçültüp patlatmak için

            //wantedScale += changeScaleValueEachHit; // Virüsü büyütüp patlatmak için

            detectControl = false;

            Invoke("openDetectControl", detectHitTime);


            //enemyColor = mutationControl.virusMaterialColors[mutationControl.nowVirusMaterialColorsNumber];
            enemyColor = mutationControl.virusMaterialColors[mutationControl.targetVirusMaterialColorsNumber];
            //Debug.Log("enemyColor: " + enemyColor);

            medicineColor = mutationControl.virusMaterialColors[medicineSwitching.currentSelectedWeapon];
            //Debug.Log("medicineColor: " + medicineColor);

            medicineAndVirusSameColorControl = (medicineColor == enemyColor);

            //Debug.Log("medicineColor == enemyColor: " + medicineAndVirusSameColorControl.ToString());

            if (medicineAndVirusSameColorControl)
            {
                hitFeelingController.hittedVirus();
            }
            else
            {
                hitFeelingController.wrongMedicine(mutationControl);
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


        if (nowScale >= maxScaleForExplosion || nowScale <= minScaleForExplosion)
        {
            Debug.Log("Enemey patladý (Öldü)");

            

            if (explodedControl == false)
            {
                //Instantiate(explosion, transform.position, Quaternion.identity);

                explosion2.SetActive(true);
                gameObject.transform.localScale = new Vector3(0, 0, 0);

                explodedControl = true;

                hitFeelingController.killedVirus();
            }


            Destroy(gameObject,2);


        }
    }
}
