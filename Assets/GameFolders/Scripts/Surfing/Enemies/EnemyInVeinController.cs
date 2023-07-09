using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInVeinController : MonoBehaviour
{
    // Virüs ya çok büyüdüðü için patlayacak (Ölecek)
    // Virüs ya da çok küçüldüðü için patlayacak (Ölecek)

    public bool hittedControl = false;

    [SerializeField] private GameObject explosion;

    public float nowScale;
    public float wantedScale;
    [SerializeField] private float changeScaleValueEachHit;

    [SerializeField] private float minScaleForExplosion; // Bu deðere ulaþýnca minimum küçülme deðerine ulaþtýðý için patlayacak
    [SerializeField] private float maxScaleForExplosion; // Bu deðere ulaþýnca maximum büyüme deðerine ulaþtýðý için patlayacak

    [SerializeField] private float changeSizeSpeed;

    [SerializeField] private float detectHitTime; // Sürekli Saldýrý olduðu için belirli bir saniyede bir vurulmasý algýmak için kullanýlacak 

    private bool detectControl;

    private void Awake()
    {
        nowScale = transform.localScale.x;

        wantedScale = nowScale;

        detectControl = true;
    }


    public void enemyGetDamaged()
    {
        if (detectControl)
        {
            Debug.Log("enemyGetDamaged çalýþtý");

            wantedScale -= changeScaleValueEachHit; // Virüsü Küçültüp patlatmak için

            //wantedScale += changeScaleValueEachHit; // Virüsü büyütüp patlatmak için

            detectControl = false;

            Invoke("openDetectControl", detectHitTime);
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


        if (nowScale >= maxScaleForExplosion || nowScale <= minScaleForExplosion)
        {
            Debug.Log("Enemey patladý (Öldü)");

            Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(gameObject);


        }
    }
}
