using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject medBot;
    [SerializeField] private GameObject virusShowCamera;
    [SerializeField] private GameObject virusDeadPartShowCamera;

    

    [SerializeField] private GameObject explosion;
    [SerializeField] private float minTimeForBetweenTwoExplosion;
    [SerializeField] private float delayForMultiExplosion; // Üst üste gerçekleþecek patlamalarda min patlama bekleme süresine eklenecek çok kýsa bir süre olacak

    [SerializeField] private GameObject enemyAlyuvarForAtackCharacter;

    [SerializeField] private BossHealthBarController bossHealthBarController;

    [SerializeField] private CameraShakeControllerInVein cameraShakeControllerInVein;

    [SerializeField] private AudioSource surfAlyuvarAudioSource;
    [SerializeField] private AudioClip explosionSound;


    [SerializeField] private int mutationLevel; //Virüs ne kadar mutasyon geçirdiyse leveli o kadar yüksektir

    [SerializeField] private float bossMaxHealthCount;
    private float bossHealthCount;
    [SerializeField] private float bossHealthCountDecreaseEveryHit;
    [SerializeField] private float bossHealthCountIncreaseEveryMutation;
    [SerializeField] private float bossHealthCountForDead;

    [SerializeField] private int minCreateEnemyAlyuvarCount;
    [SerializeField] private int maxCreateEnemyAlyuvarCount;
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
    [SerializeField] private float delayHideBossHealthBarAfterCompleteMutation;

    [SerializeField] private bool changeColorControl;
    [SerializeField] private bool changeColorCompleteControl;
    [SerializeField] private float waitTimeForChangeColorAfterSockWave;
    private float changeColorSpeed;
    [Range(0.1f, 10f)] [SerializeField] private float minChangeColorSpeed;
    [Range(0.1f, 10f)] [SerializeField] private float maxChangeColorSpeed;

    private bool reachMaxSize = false;

    private bool showExplosionControl = false;

    private float targetSize;

    public int nowVirusMaterialColorsNumber = -1;
    public int targetVirusMaterialColorsNumber = -1;

    private Material virusMaterial;

    public Color[] virusMaterialColors;

    [SerializeField] private GameObject[] bossHealthBarParts;


    [SerializeField] private bool alyuvarEnemyCreateControl;
    private bool alyuvarEnemyCreateCompleteControl;


    [Header("Mutation Level")]
    // Dizilerdeki 0. index level 1  - 1. index level 2 yi gösterir bu þekilde devam eder
    [SerializeField] private float[] minWaitTimeChangeColorAccordiongToMutationLevel;
    [SerializeField] private float[] maxWaitTimeChangeColorAccordiongToMutationLevel;
    [SerializeField] private int[] alyuvarAtackFieldCount;
    [SerializeField] private float[] minWaitTimeAtackWithAlyuvarAccordiongToMutationLevel;
    [SerializeField] private float[] maxWaitTimeAtackWithAlyuvarAccordiongToMutationLevel;

    private float waitTimeChangeColor = 0;
    private float waitTimeAtackWithAlyuvar = 0;


    [Header("Boss Dead")]
    [SerializeField] private int sockWaveCount;
    [SerializeField] private GameObject finalExplosion;
    [SerializeField] private float waitTimeForFinalExplosionAfterBossSmallAndDestroy;
    [SerializeField] private float waitTimeAfterAllSockWaveComplete;
    [SerializeField] private float waitTimeForHideMedBot;
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float minSizeForExplode;
    private bool bossDeadControl = false;


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

        alyuvarEnemyCreateCompleteControl = true;

        changeColorControl = false;


        alyuvarEnemyCreateControl = false;


        bossHealthCount = bossMaxHealthCount;
        bossHealthBarController.SetMaxHealth(bossHealthCount);


        //showExplosion();

        //StartCoroutine(createEnemyAlyuvars());
    }

    private void showExplosionMultiTime(int explosionCount)
    {
        for(int i = 0; i < explosionCount; i++)
        {
            // ilk patlama beklemeden gerçekleþecek
            if(i == 0)
            {
                Invoke("showExplosion", minTimeForBetweenTwoExplosion * i);
            }
            else
            {
                Invoke("showExplosion", minTimeForBetweenTwoExplosion * i + delayForMultiExplosion);

            }
        }
    }

    private void showExplosion()
    {
        if(showExplosionControl == false)
        {
            showExplosionControl = true;
            explosion.SetActive(true);

            surfAlyuvarAudioSource.PlayOneShot(explosionSound);

            Invoke("activateShowExplosionControl", minTimeForBetweenTwoExplosion);
        }
    }

    private void activateShowExplosionControl()
    {
        showExplosionControl = false;
        explosion.SetActive(false);
    }


    public void mutateBossVirus()
    {
        mutationLevel++;

        changeVirusColor();

        bossMutatedIncreaseHealth();
    }


    private void changeVirusColor()
    {
        //showExplosionMultiTime(5);

        if(changeColorCompleteControl == true)
        {
            showExplosion();

            //changeColorControl = true;
            Invoke("changeChangeColoControl", waitTimeForChangeColorAfterSockWave);
        }
    }
    
    private void changeChangeColoControl()
    {
        changeColorControl = true;
    }

    private void alyuvarEnemyCreate()
    {
        //showExplosion();

        


        if (alyuvarEnemyCreateCompleteControl)
        {
            //showExplosionMultiTime(2);
            //showExplosionMultiTime(alyuvarAtackFieldCount[mutationLevel]);

            Debug.Log("alyuvarEnemyCreate çalýþtý: " + Time.time.ToString());

            alyuvarEnemyCreateControl = true;
            alyuvarEnemyCreateCompleteControl = false;
        }
        
    }


    private IEnumerator mutateBySelfControl()
    {
        yield return null;

        float waitSumTimeForChangeColor = 0;
        float waitSumTimeForAtackWithAlyuvar = 0;
        float waitTimeEachControl = 0.1f;

        while (true)
        {
            yield return new WaitForSeconds(waitTimeEachControl);

            waitSumTimeForChangeColor += waitTimeEachControl;
            waitSumTimeForAtackWithAlyuvar += waitTimeEachControl;

            if(mutationLevel > minWaitTimeChangeColorAccordiongToMutationLevel.Length - 1)
            {
                mutationLevel = minWaitTimeChangeColorAccordiongToMutationLevel.Length - 1;
            }


            // Renk Deðiþme ----------------------------------------------------------------------------------------------------------
            if (waitTimeChangeColor == 0 || waitTimeChangeColor >= minWaitTimeChangeColorAccordiongToMutationLevel[mutationLevel])
            {
                waitTimeChangeColor = Random.Range(minWaitTimeChangeColorAccordiongToMutationLevel[mutationLevel], 
                    maxWaitTimeChangeColorAccordiongToMutationLevel[mutationLevel]);
            }


            if(waitSumTimeForChangeColor >= waitTimeChangeColor)
            {
                changeVirusColor();

                waitSumTimeForChangeColor = 0;
                waitTimeChangeColor = 0;
            }
            // --------------------------------------------------------------------------------------------------------------------------


            // Alyuvar Ýle Saldýrý Yapam ------------------------------------------------------------------------------------------------
            if (waitTimeAtackWithAlyuvar == 0 || waitTimeAtackWithAlyuvar >= minWaitTimeAtackWithAlyuvarAccordiongToMutationLevel[mutationLevel])
            {
                waitTimeAtackWithAlyuvar = Random.Range(minWaitTimeAtackWithAlyuvarAccordiongToMutationLevel[mutationLevel],
                    maxWaitTimeAtackWithAlyuvarAccordiongToMutationLevel[mutationLevel]);
            }

            if (waitSumTimeForAtackWithAlyuvar >= waitTimeAtackWithAlyuvar)
            {
                alyuvarEnemyCreate();

                waitSumTimeForAtackWithAlyuvar = 0;
                waitTimeAtackWithAlyuvar = 0;
            }

            // --------------------------------------------------------------------------------------------------------------------------

        }
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

        if (changeColorControl && changeColorCompleteControl && bossDeadControl == false)
        {
            //Debug.Log("Renk deðiþtirme Baþladý");

            StartCoroutine(changeColorVirus());

            changeColorControl = false;
            changeColorCompleteControl = false;
        }


        if (alyuvarEnemyCreateControl && bossDeadControl == false)
        {
            //StartCoroutine(createEnemyAlyuvars());

            if (mutationLevel > minWaitTimeChangeColorAccordiongToMutationLevel.Length - 1)
            {
                mutationLevel = minWaitTimeChangeColorAccordiongToMutationLevel.Length - 1;
            }

            createEnemyAlyuvarsDifferentFields(alyuvarAtackFieldCount[mutationLevel]);


            alyuvarEnemyCreateControl = false;
        }
    }


    public void bossGetDamaged()
    {
        if(mutationLevel != 0)
        {
            bossHealthCount -= bossHealthCountDecreaseEveryHit * mutationLevel;
        }
        else
        {
            bossHealthCount -= bossHealthCountDecreaseEveryHit;
        }

        bossHealthBarController.SetHealth(bossHealthCount);


        if(bossHealthCount <= bossHealthCountForDead)
        {
            // Boss Öldü
            if (bossDeadControl == false)
            {
                Debug.Log("Boss Öldü");

                Invoke("hideMedBot", waitTimeForHideMedBot);

                bossDead();
                bossDeadControl = true;
            }
            
        }
    }

    private void hideMedBot()
    {
        medBot.SetActive(false);
    }

    private void bossDead()
    {
        virusDeadPartShowCamera.SetActive(true);

        Invoke("showBossHealthBar", delayShowBossHealthBarAfterCompleteMutation);

        showExplosionMultiTime(sockWaveCount);

        //Invoke("showFinalExplosion", (minTimeForBetweenTwoExplosion * sockWaveCount) + (delayForMultiExplosion * (sockWaveCount - 2)) + waitTimeAfterAllSockWaveComplete);
        Invoke("startShrinkBoss", (minTimeForBetweenTwoExplosion * sockWaveCount) + (delayForMultiExplosion * (sockWaveCount - 2)) + waitTimeAfterAllSockWaveComplete);
    }

    private void startShrinkBoss()
    {

    }

    private IEnumerator shrinkBoss()
    {
        while (transform.localScale.x > minSizeForExplode)
        {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f) * shrinkSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        

        Invoke("showFinalExplosion", waitTimeForFinalExplosionAfterBossSmallAndDestroy);
    }

    private void showFinalExplosion()
    {
        finalExplosion.SetActive(true);
        
    }

    public void bossMutatedIncreaseHealth()
    {
        bossHealthCount += bossHealthCountIncreaseEveryMutation;


        if (bossHealthCount > bossMaxHealthCount)
        {
            bossHealthCount = bossMaxHealthCount;
        }

        bossHealthBarController.SetHealth(bossHealthCount);
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

    private void hideBossHealthBar()
    {
        foreach (GameObject gameObjects in bossHealthBarParts)
        {
            gameObjects.SetActive(false);
        }
    }



    public void magnificationBoss()
    {
        targetSize += magnificationSizeEachIngestCell;

        

        if (reachMaxSize == false)
        {
            cameraShakeControllerInVein.cameraShake(cameraShakeIntensityForCreatingBoss, cameraShakefullIntensityTimeForCreatingBoss, cameraShakeGoToZeroTimeForCreatingBoss);

            dnaImageShow.showDna();

            showExplosion();

        }
        else
        {
            StartCoroutine(mutateBySelfControl());
        }
    }


    private void createEnemyAlyuvarsDifferentFields(int fieldCount)
    {
        HashSet<int> uniqueValues = new HashSet<int>();

        for(int i = 0; i < fieldCount; i = uniqueValues.Count)
        {
            uniqueValues.Add(Random.Range(1, 5));
        }

        int[] uniqueValuesArray = new int[uniqueValues.Count];

        int j = 0;
        foreach (int value in uniqueValues)
        {
            Debug.Log("uniqueValues: " + value);
            //StartCoroutine(createEnemyAlyuvars(value));

            uniqueValuesArray[j++] = value;
        }


        //StartCoroutine(createEnemyAlyuvars(new int[4] {1,2,3,4}));
        StartCoroutine(createEnemyAlyuvars(uniqueValuesArray));
        //StartCoroutine(createEnemyAlyuvars(2));
    }

    IEnumerator createEnemyAlyuvars(int[] randomNumbers)
    {
        yield return null;

        Debug.Log("createEnemyAlyuvars çalýþtý ");

        int nowCreatedEnemyCount = 0;

        Vector3 enemyPosition;

        //createEnemyAlyuvarCount = Random.Range(minCreateEnemyAlyuvarCount, maxCreateEnemyAlyuvarCount);

        int createEnemyAlyuvarCount2 = Random.Range(minCreateEnemyAlyuvarCount, maxCreateEnemyAlyuvarCount);

        // 4 Farklý Bölgede Çýksýn
        // 1. Bölge Sað - Üst
        // 2. Bölge Sað - Alt
        // 3. Bölge Sol - Alt
        // 4. Bölge Sol - Üst

        //int randomNumber = Random.Range(1, 5);

        /*
        switch (randomNumber)
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
        */

        float xMinFarFromCenterForCreatingEnemyAlyuvarBeforeValue;
        float saveFloatValue; // Ýki deðiþkeni birbirleriyle deðiþtirmek için kullanýlacak

        while (nowCreatedEnemyCount < createEnemyAlyuvarCount2)
        {
            for(int i = 0; i< Random.Range(1, maxCreateEnemyAlyuvarEveryBetwewnWaitTime); i++)
            {
                //foreach (int randomNumber in randomNumbers)


                for (int j = 0; j < randomNumbers.Length; j++)
                {
                    Debug.Log("j sayýsý: " + j.ToString());

                    Debug.Log("randomNumbers[j]) sayýsý: " + randomNumbers[j].ToString());

                    switch (randomNumbers[j])
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

                            saveFloatValue = xMinFarFromCenterForCreatingEnemyAlyuvar;

                            xMinFarFromCenterForCreatingEnemyAlyuvar = xMaxFarFromCenterForCreatingEnemyAlyuvar * -1;
                            xMaxFarFromCenterForCreatingEnemyAlyuvar = saveFloatValue * -1;

                            yMinFarFromCenterForCreatingEnemyAlyuvar *= 1;
                            yMaxFarFromCenterForCreatingEnemyAlyuvar *= 1;

                            break;
                        case 3:
                            // 3. Bölge Sol - Alt

                            saveFloatValue = xMinFarFromCenterForCreatingEnemyAlyuvar;

                            xMinFarFromCenterForCreatingEnemyAlyuvar = xMaxFarFromCenterForCreatingEnemyAlyuvar  * - 1;
                            xMaxFarFromCenterForCreatingEnemyAlyuvar = saveFloatValue * - 1;

                            saveFloatValue = yMinFarFromCenterForCreatingEnemyAlyuvar;

                            yMinFarFromCenterForCreatingEnemyAlyuvar = yMaxFarFromCenterForCreatingEnemyAlyuvar  * - 1;
                            yMaxFarFromCenterForCreatingEnemyAlyuvar = saveFloatValue  * - 1;

                            break;
                        case 4:
                            // 4. Bölge Sol - Üst

                            xMinFarFromCenterForCreatingEnemyAlyuvar *= 1;
                            xMaxFarFromCenterForCreatingEnemyAlyuvar *= 1;

                            saveFloatValue = yMinFarFromCenterForCreatingEnemyAlyuvar;

                            yMinFarFromCenterForCreatingEnemyAlyuvar = yMaxFarFromCenterForCreatingEnemyAlyuvar * -1;
                            yMaxFarFromCenterForCreatingEnemyAlyuvar = saveFloatValue * -1;

                            break;
                    }


                    //Debug.Log("xMinFarFromCenterForCreatingEnemyAlyuvar1: " + xMinFarFromCenterForCreatingEnemyAlyuvar);
                    //Debug.Log("xMaxFarFromCenterForCreatingEnemyAlyuvar1: " + xMaxFarFromCenterForCreatingEnemyAlyuvar);

                    /*
                    if (xMinFarFromCenterForCreatingEnemyAlyuvar > xMaxFarFromCenterForCreatingEnemyAlyuvar)
                    {
                        xMinFarFromCenterForCreatingEnemyAlyuvarBeforeValue = xMinFarFromCenterForCreatingEnemyAlyuvar;

                        xMinFarFromCenterForCreatingEnemyAlyuvar = xMaxFarFromCenterForCreatingEnemyAlyuvar;
                        xMaxFarFromCenterForCreatingEnemyAlyuvar = xMinFarFromCenterForCreatingEnemyAlyuvarBeforeValue;
                    }

                    Debug.Log("xMinFarFromCenterForCreatingEnemyAlyuvar2: " + xMinFarFromCenterForCreatingEnemyAlyuvar);
                    Debug.Log("xMaxFarFromCenterForCreatingEnemyAlyuvar2: " + xMaxFarFromCenterForCreatingEnemyAlyuvar);
                    */

                    enemyPosition = new Vector3(transform.position.x + Random.Range(xMinFarFromCenterForCreatingEnemyAlyuvar, xMaxFarFromCenterForCreatingEnemyAlyuvar),
                    transform.position.y + Random.Range(yMinFarFromCenterForCreatingEnemyAlyuvar, yMaxFarFromCenterForCreatingEnemyAlyuvar),
                    transform.position.z);

                    //Instantiate(enemyAlyuvarForAtackCharacter, transform.position, Quaternion.identity);
                    Instantiate(enemyAlyuvarForAtackCharacter, enemyPosition, Quaternion.identity);


                    switch (randomNumbers[j])
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

                            saveFloatValue = xMinFarFromCenterForCreatingEnemyAlyuvar;

                            xMinFarFromCenterForCreatingEnemyAlyuvar = xMaxFarFromCenterForCreatingEnemyAlyuvar * -1;
                            xMaxFarFromCenterForCreatingEnemyAlyuvar = saveFloatValue * -1;

                            yMinFarFromCenterForCreatingEnemyAlyuvar *= 1;
                            yMaxFarFromCenterForCreatingEnemyAlyuvar *= 1;

                            break;
                        case 3:
                            // 3. Bölge Sol - Alt

                            saveFloatValue = xMinFarFromCenterForCreatingEnemyAlyuvar;

                            xMinFarFromCenterForCreatingEnemyAlyuvar = xMaxFarFromCenterForCreatingEnemyAlyuvar * -1;
                            xMaxFarFromCenterForCreatingEnemyAlyuvar = saveFloatValue * -1;

                            saveFloatValue = yMinFarFromCenterForCreatingEnemyAlyuvar;

                            yMinFarFromCenterForCreatingEnemyAlyuvar = yMaxFarFromCenterForCreatingEnemyAlyuvar * -1;
                            yMaxFarFromCenterForCreatingEnemyAlyuvar = saveFloatValue * -1;

                            break;
                        case 4:
                            // 4. Bölge Sol - Üst

                            xMinFarFromCenterForCreatingEnemyAlyuvar *= 1;
                            xMaxFarFromCenterForCreatingEnemyAlyuvar *= 1;

                            saveFloatValue = yMinFarFromCenterForCreatingEnemyAlyuvar;

                            yMinFarFromCenterForCreatingEnemyAlyuvar = yMaxFarFromCenterForCreatingEnemyAlyuvar * -1;
                            yMaxFarFromCenterForCreatingEnemyAlyuvar = saveFloatValue * -1;

                            break;
                    }


                    //Debug.Log("xMinFarFromCenterForCreatingEnemyAlyuvar2: " + xMinFarFromCenterForCreatingEnemyAlyuvar);
                    //Debug.Log("xMaxFarFromCenterForCreatingEnemyAlyuvar2: " + xMaxFarFromCenterForCreatingEnemyAlyuvar);


                }


                nowCreatedEnemyCount++;
            }
            

            yield return new WaitForSeconds(waitTimeForCreateEnemyAlyuvar);
        }

        alyuvarEnemyCreateCompleteControl = true;
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
