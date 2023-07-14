using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject medBot;
    [SerializeField] private GameObject virusShowCamera;

    [SerializeField] private GameObject enemyAlyuvarForAtackCharacter;

    [SerializeField] private BossHealthBarController bossHealthBarController;

    [SerializeField] private CameraShakeControllerInVein cameraShakeControllerInVein;

    [SerializeField] private float bossHealthCount;
    [SerializeField] private float bossHealthCountDecreaseEveryHit;
    [SerializeField] private float bossHealthCountIncreaseEveryMutation;
    [SerializeField] private float bossHealthCountForDead;

    [SerializeField] private float minCreateEnemyAlyuvarCount;
    [SerializeField] private float maxCreateEnemyAlyuvarCount;
    private float createEnemyAlyuvarCount;

    [SerializeField] private float waitTimeForCreateEnemyAlyuvar;

    [SerializeField] private float maxCreateEnemyAlyuvarEveryBetwewnWaitTime;

    [SerializeField] private float xMinFarFromCenterForCreatingEnemyAlyuvar;
    [SerializeField] private float xMaxFarFromCenterForCreatingEnemyAlyuvar;
    [SerializeField] private float yMinFarFromCenterForCreatingEnemyAlyuvar;
    [SerializeField] private float yMaxFarFromCenterForCreatingEnemyAlyuvar;




    [SerializeField] private float cameraShakeIntensityForCreatingBoss;
    [SerializeField] private float cameraShakefullIntensityTimeForCreatingBoss;
    [SerializeField] private float cameraShakeGoToZeroTimeForCreatingBoss;



    [SerializeField] private float maxSizeBoss;

    [SerializeField] private float magnificationSizeSpeed;

    [SerializeField] private float magnificationSizeEachIngestCell;

    [SerializeField] DnaImageShow dnaImageShow;

    [SerializeField] private float delayActivateMedbotAfterCompleteMutation;
    [SerializeField] private float delayShowBossHealthBarAfterCompleteMutation;

    [SerializeField] private bool changeColorControl;
    [SerializeField] private bool changeColorCompleteControl;
    private float changeColorSpeed;
    [Range(0.1f, 10f)] [SerializeField] private float minChangeColorSpeed;
    [Range(0.1f, 10f)] [SerializeField] private float maxChangeColorSpeed;

    private bool reachMaxSize = false;

    private float targetSize;

    public int nowVirusMaterialColorsNumber = -1;
    public int targetVirusMaterialColorsNumber = -1;

    private Material virusMaterial;

    public Color[] virusMaterialColors;

    [SerializeField] private GameObject[] bossHealthBarParts;


    [SerializeField] private bool alyuvarEnemyCreateControl;


    void Awake()
    {
        virusMaterial = gameObject.GetComponent<Renderer>().material;

        if (nowVirusMaterialColorsNumber == -1)
        {
            nowVirusMaterialColorsNumber = Random.Range(0, virusMaterialColors.Length);
        }
        virusMaterial.color = virusMaterialColors[nowVirusMaterialColorsNumber];

        targetVirusMaterialColorsNumber = nowVirusMaterialColorsNumber;

        changeColorCompleteControl = true;

        changeColorControl = false;


        alyuvarEnemyCreateControl = false;


        bossHealthBarController.SetMaxHealth(bossHealthCount);


        //StartCoroutine(createEnemyAlyuvars());
    }



    void Update()
    {
        if(transform.localScale.x < targetSize && reachMaxSize == false)
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * magnificationSizeSpeed * Time.deltaTime;
        }

        if(transform.localScale.x >= maxSizeBoss)
        {
            reachMaxSize = true;

            dnaImageShow.hideDna();

            Invoke("activateMedBot", delayActivateMedbotAfterCompleteMutation);
            Invoke("showBossHealthBar", delayShowBossHealthBarAfterCompleteMutation);
        }

        if (changeColorControl && changeColorCompleteControl)
        {
            //Debug.Log("Renk deðiþtirme Baþladý");

            StartCoroutine(changeColorVirus());

            changeColorControl = false;
        }


        if (alyuvarEnemyCreateControl)
        {
            StartCoroutine(createEnemyAlyuvars());

            alyuvarEnemyCreateControl = false;
        }
    }


    public void bossGetDamaged()
    {
        bossHealthCount -= bossHealthCountDecreaseEveryHit;

        bossHealthBarController.SetHealth(bossHealthCount);


        if(bossHealthCount <= bossHealthCountForDead)
        {
            // Boss Öldü
        }
    }

    private void activateMedBot()
    {
        //medBot.SetActive(true);
        //medBot.GetComponent<ThirdPersonShooterController>().enabled = true;
        virusShowCamera.SetActive(false);
    }

    private void showBossHealthBar()
    {
        foreach (GameObject gameObjects in bossHealthBarParts)
        {
            gameObjects.SetActive(true);
        }
    }


    public void magnificationBoss()
    {
        targetSize += magnificationSizeEachIngestCell;

        

        if (reachMaxSize == false)
        {
            cameraShakeControllerInVein.cameraShake(cameraShakeIntensityForCreatingBoss, cameraShakefullIntensityTimeForCreatingBoss, cameraShakeGoToZeroTimeForCreatingBoss);

            dnaImageShow.showDna();

        }
    }

    IEnumerator createEnemyAlyuvars()
    {
        yield return null;

        //yield return new WaitForSeconds(6);

        int nowCreatedEnemyCount = 0;

        Vector3 enemyPosition;

        createEnemyAlyuvarCount = Random.Range(minCreateEnemyAlyuvarCount, maxCreateEnemyAlyuvarCount);

        // 4 Farklý Bölgede Çýksýn
        // 1. Bölge Sað - Üst
        // 2. Bölge Sað - Alt
        // 3. Bölge Sol - Alt
        // 4. Bölge Sol - Üst



        switch (Random.Range(1, 5))
        {
            case 1:
                // 1. Bölge Sað - Üst

                xMinFarFromCenterForCreatingEnemyAlyuvar *= 1;
                xMaxFarFromCenterForCreatingEnemyAlyuvar *= 1;

                yMinFarFromCenterForCreatingEnemyAlyuvar *= 1;
                yMaxFarFromCenterForCreatingEnemyAlyuvar *= 1;

                break;
            case 2:
                // 2. Bölge Sað - Alt

                xMinFarFromCenterForCreatingEnemyAlyuvar *= 1;
                xMaxFarFromCenterForCreatingEnemyAlyuvar *= 1;

                yMinFarFromCenterForCreatingEnemyAlyuvar *= -1;
                yMaxFarFromCenterForCreatingEnemyAlyuvar *= -1;

                break;
            case 3:
                // 3. Bölge Sol - Alt

                xMinFarFromCenterForCreatingEnemyAlyuvar *= -1;
                xMaxFarFromCenterForCreatingEnemyAlyuvar *= -1;

                yMinFarFromCenterForCreatingEnemyAlyuvar *= -1;
                yMaxFarFromCenterForCreatingEnemyAlyuvar *= -1;

                break;
            case 4:
                // 4. Bölge Sol - Üst

                xMinFarFromCenterForCreatingEnemyAlyuvar *= -1;
                xMaxFarFromCenterForCreatingEnemyAlyuvar *= -1;

                yMinFarFromCenterForCreatingEnemyAlyuvar *= 1;
                yMaxFarFromCenterForCreatingEnemyAlyuvar *= 1;

                break;

        }



        while (nowCreatedEnemyCount < createEnemyAlyuvarCount)
        {
            for(int i = 0; i< Random.Range(1, maxCreateEnemyAlyuvarEveryBetwewnWaitTime); i++)
            {
                enemyPosition = new Vector3(transform.position.x + Random.Range(xMinFarFromCenterForCreatingEnemyAlyuvar, xMaxFarFromCenterForCreatingEnemyAlyuvar),
                transform.position.y + Random.Range(yMinFarFromCenterForCreatingEnemyAlyuvar, yMaxFarFromCenterForCreatingEnemyAlyuvar),
                transform.position.z);

                //Instantiate(enemyAlyuvarForAtackCharacter, transform.position, Quaternion.identity);
                Instantiate(enemyAlyuvarForAtackCharacter, enemyPosition, Quaternion.identity);

                nowCreatedEnemyCount++;
            }
            

            yield return new WaitForSeconds(waitTimeForCreateEnemyAlyuvar);
        }



    }

    IEnumerator changeColorVirus()
    {
        changeColorCompleteControl = false;

        changeColorSpeed = Random.Range(minChangeColorSpeed, maxChangeColorSpeed);

        targetVirusMaterialColorsNumber = Random.Range(0, virusMaterialColors.Length);
        while (targetVirusMaterialColorsNumber == nowVirusMaterialColorsNumber)
        {
            targetVirusMaterialColorsNumber = Random.Range(0, virusMaterialColors.Length);
        }

        while (true)
        {
            //virusMaterial.color = Color.Lerp(virusMaterial.color, virusMaterialColors[2], 0.01f);
            virusMaterial.color = Color.Lerp(virusMaterial.color, virusMaterialColors[targetVirusMaterialColorsNumber], changeColorSpeed / 100);

            yield return null;

            //Debug.Log("Renk Deðiþiyor");

            if (virusMaterial.color == virusMaterialColors[targetVirusMaterialColorsNumber])
            {
                nowVirusMaterialColorsNumber = targetVirusMaterialColorsNumber;
                changeColorCompleteControl = true;
                //Debug.Log("Hedef Renge Ulaþýldý");
                break;
            }
        }

    }


}
