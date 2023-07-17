using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject medBot;
    [SerializeField] private GameObject virusShowCamera;
    [SerializeField] private GameObject virusDeadPartShowCamera;

    [SerializeField] private SurfingControllerV2 surfingControllerV2;

    [SerializeField] private float maxHealthForMaxMutationLevel;

    private bool virusShowCameraWorkedForFirstContactWithBoss;

    private bool startMagnificationControl = false;

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
    [SerializeField] private float zMinFarFromCenterForCreatingEnemyAlyuvar;
    [SerializeField] private float zMaxFarFromCenterForCreatingEnemyAlyuvar;




    [SerializeField] private float cameraShakeIntensityForCreatingBoss;
    [SerializeField] private float cameraShakefullIntensityTimeForCreatingBoss;
    [SerializeField] private float cameraShakeGoToZeroTimeForCreatingBoss;



    [SerializeField] private float maxSizeBoss;

    [SerializeField] private float magnificationSizeSpeed;

    [SerializeField] private float magnificationSizeEachIngestCell;

    [SerializeField] DnaImageShow dnaImageShow;

    [SerializeField] private float delayActivateMedbotAfterCompleteMutation;
    [SerializeField] private float delayShowBossHealthBarAfterCompleteMutation;
    [SerializeField] private float delayHideBossHealthBarAfterBossDead;

    private bool showBossHealthBarCompleteControl = false;

    [SerializeField] private bool changeColorControl;
    [SerializeField] private bool changeColorCompleteControl;
    [SerializeField] private float waitTimeForChangeColorAfterSockWave;
    private float changeColorSpeed;
    [Range(0.1f, 10f)] [SerializeField] private float minChangeColorSpeed;
    [Range(0.1f, 10f)] [SerializeField] private float maxChangeColorSpeed;

    private bool mutationLevelIncreaseControl = true;
    [SerializeField] private float waitTimeChangeMutationLevelIncreaseControl;

    //private bool changeColorBySelfMutationControl = false;

    public bool reachMaxSize = false;

    private bool mutateBySelfWorkControl = false;

    private bool showExplosionControl = false;

    private float targetSize;

    public int nowVirusMaterialColorsNumber = -1;
    public int targetVirusMaterialColorsNumber = -1;

    private Material virusMaterial;

    public Color[] virusMaterialColors;

    [SerializeField] private GameObject[] bossHealthBarParts;
    [SerializeField] private GameObject[] canvasHealthAndMedicineImages;


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
    [SerializeField] private float waitTimeHideVirusCameraAfterFinalExplosion;
    [SerializeField] private float waitTimeForFinalExplosionAfterBossSmallAndDestroy;
    [SerializeField] private float waitTimeAfterAllSockWaveComplete;
    [SerializeField] private float waitTimeForHideMedBot;
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float minSizeForExplode;
    [SerializeField] private float MaxWaitTimeForExplodeEnemyAlyuvarForBossDead;
    [SerializeField] private float waitTimeForRunBossDead;

    private bool bossDeadControl = false;


    void Awake()
    {
        virusShowCameraWorkedForFirstContactWithBoss = false;

        virusMaterial = gameObject.GetComponent<Renderer>().material;

        //enemyAlyuvarForAtackCharacter = GameObject.Find("Enemy Alyuvar02 For Atack Character").gameObject;
        enemyAlyuvarForAtackCharacter = GameObject.Find("Enemy Alyuvar02 For Atack Character with explosion").gameObject;

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

        StartCoroutine(firstContactshowExplosion());

        //virusShowCamera.SetActive(true);


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

    public void mutateBossVirusNotBySelf()
    {
        //changeVirusColor();

        if(changeColorCompleteControl == true)
        {
            //changeColorControl = true;
        }
        

        bossMutatedIncreaseHealth();

        Debug.Log("Mutasyon Algýnlandý Renk Deðiþecek");

    }

    public void mutateBossVirus()
    {
        if (mutationLevelIncreaseControl)
        {
            mutationLevel++;

            //changeVirusColor();

            changeColorControl = true;

            bossMutatedIncreaseHealth();

            Debug.Log("Mutasyon Algýnlandý Renk Deðiþecek");

            mutationLevelIncreaseControl = false;
            Invoke("changeMutationLevelIncreaseControl", waitTimeChangeMutationLevelIncreaseControl);
        }
        
    }

    private void changeMutationLevelIncreaseControl()
    {
        mutationLevelIncreaseControl = true;
    }


    private void changeVirusColor()
    {
        //showExplosionMultiTime(5);

        //Debug.Log("Renk Deðiþmeye baþlýyor");

        if(changeColorCompleteControl == true)
        {
            showExplosion();

            //Debug.Log("Renk Deðiþmeye baþladý");

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

        Debug.Log("mutateBySelfControl çalýþmaya baþladý");

        float waitSumTimeForChangeColor = 0;
        float waitSumTimeForAtackWithAlyuvar = 0;
        float waitTimeEachControl = 0.1f;

        while (true && bossDeadControl == false)
        {
            yield return new WaitForSeconds(waitTimeEachControl);

            Debug.Log("mutateBySelfControl çalýþmaya devam ediyor");

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

        if (surfingControllerV2.contactBossControl)
        {
            if (virusShowCameraWorkedForFirstContactWithBoss == false && bossDeadControl == false)
            {
                virusShowCamera.SetActive(true);

                virusShowCameraWorkedForFirstContactWithBoss = true;
            }
            
        }


        if(transform.localScale.x < targetSize && reachMaxSize == false)
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * magnificationSizeSpeed * Time.deltaTime;
        }

        if(transform.localScale.x >= maxSizeBoss)
        {
            Debug.Log("transform.localScale.x >= maxSizeBoss");

            reachMaxSize = true;

            if(mutateBySelfWorkControl == false)
            {
                StartCoroutine(mutateBySelfControl());

                mutateBySelfWorkControl = true;
            }
            

            dnaImageShow.hideDna();

            Invoke("activateMedBot", delayActivateMedbotAfterCompleteMutation);
            Invoke("showBossHealthBar", delayShowBossHealthBarAfterCompleteMutation);
        }

        if (changeColorControl && changeColorCompleteControl && bossDeadControl == false)
        {
            Debug.Log("Mutasyon Renk deðiþtirme Baþladý");

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



           
        if(maxHealthForMaxMutationLevel >= bossHealthCount)
        {
            mutationLevel = minWaitTimeChangeColorAccordiongToMutationLevel.Length - 1;
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

                Invoke("bossDead", waitTimeForRunBossDead);



                EnemyAlyuvarController[] objects = GameObject.FindObjectsOfType<EnemyAlyuvarController>();

                foreach (EnemyAlyuvarController obj in objects)
                {
                    obj.destroyAndExplode(Random.Range(0, MaxWaitTimeForExplodeEnemyAlyuvarForBossDead));
                }

                //bossDead();
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
        //virusShowCamera.SetActive(true);

        Invoke("hideBossHealthBar", delayHideBossHealthBarAfterBossDead);

        showExplosionMultiTime(sockWaveCount);

        //Invoke("showFinalExplosion", (minTimeForBetweenTwoExplosion * sockWaveCount) + (delayForMultiExplosion * (sockWaveCount - 2)) + waitTimeAfterAllSockWaveComplete);
        Invoke("startShrinkBoss", (minTimeForBetweenTwoExplosion * sockWaveCount) + (delayForMultiExplosion * (sockWaveCount - 2)) + waitTimeAfterAllSockWaveComplete);
    }

    private void startShrinkBoss()
    {
        StartCoroutine(shrinkBoss());
    }

    private IEnumerator shrinkBoss()
    {
        while (transform.localScale.x > minSizeForExplode)
        {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f) * shrinkSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.localScale = new Vector3(0, 0, 0);
        bossDeadControl = true;



        Invoke("showFinalExplosion", waitTimeForFinalExplosionAfterBossSmallAndDestroy);
    }

    private void showFinalExplosion()
    {
        finalExplosion.SetActive(true);
        surfAlyuvarAudioSource.PlayOneShot(explosionSound);
        surfAlyuvarAudioSource.PlayOneShot(explosionSound);
        surfAlyuvarAudioSource.PlayOneShot(explosionSound);

        Invoke("hideVirusShowCamera", waitTimeHideVirusCameraAfterFinalExplosion);
    }

    private void hideVirusShowCamera()
    {
        
        virusShowCamera.SetActive(false);
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

        if (showBossHealthBarCompleteControl == false)
        {
            showBossHealthBarCompleteControl = true;

            Debug.Log("Boss Caný Fullendi");
            bossHealthCount = bossMaxHealthCount;
            bossHealthBarController.SetMaxHealth(bossHealthCount);

            mutationLevel = 0;

            foreach (GameObject gameObjects in canvasHealthAndMedicineImages)
            {
                gameObjects.SetActive(true);
            }

            foreach (GameObject gameObjects in bossHealthBarParts)
            {
                gameObjects.SetActive(true);
            }
        }
        


        
    }

    private void hideBossHealthBar()
    {
        foreach (GameObject gameObjects in canvasHealthAndMedicineImages)
        {
            gameObjects.SetActive(false);
        }

        foreach (GameObject gameObjects in bossHealthBarParts)
        {
            gameObjects.SetActive(false);
        }
    }


    IEnumerator firstContactshowExplosion()
    {
        yield return new WaitForSeconds(0.6f);

        while (true)
        {
            if (surfingControllerV2.contactBossControl)
            {
                if (startMagnificationControl == false)
                {
                    showExplosion();
                }
                else
                {
                    break;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
        
    }



    public void magnificationBoss()
    {
        startMagnificationControl = true;

        targetSize += magnificationSizeEachIngestCell;

        Debug.Log("Virüs Büyümeye Baþladý");

        if (reachMaxSize == false)
        {
            cameraShakeControllerInVein.cameraShake(cameraShakeIntensityForCreatingBoss, cameraShakefullIntensityTimeForCreatingBoss, cameraShakeGoToZeroTimeForCreatingBoss);

            dnaImageShow.showDna();

            showExplosion();

            Debug.Log("Virüs reachMaxSize hala ulaþýlmadý");

        }
        else
        {
            Debug.Log("Virüs reachMaxSize ulaþtý ");

            //StartCoroutine(mutateBySelfControl());
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

        Debug.Log("uniqueValuesArray.Length: " + uniqueValuesArray.Length);
        Debug.Log("uniqueValuesArray fieldCount: " + fieldCount);


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

        while (nowCreatedEnemyCount < createEnemyAlyuvarCount2 && bossDeadControl == false)
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

                    /*
                    enemyPosition = new Vector3(transform.position.x + Random.Range(xMinFarFromCenterForCreatingEnemyAlyuvar, xMaxFarFromCenterForCreatingEnemyAlyuvar),
                    transform.position.y + Random.Range(yMinFarFromCenterForCreatingEnemyAlyuvar, yMaxFarFromCenterForCreatingEnemyAlyuvar),
                    transform.position.z);
                    */

                    enemyPosition = new Vector3(477,
                    transform.position.y + Random.Range(yMinFarFromCenterForCreatingEnemyAlyuvar, yMaxFarFromCenterForCreatingEnemyAlyuvar),
                    3472.5f + Random.Range(xMinFarFromCenterForCreatingEnemyAlyuvar, xMaxFarFromCenterForCreatingEnemyAlyuvar));


                    

                    //Instantiate(enemyAlyuvarForAtackCharacter, transform.position, Quaternion.identity);
                    if(bossDeadControl == false)
                    {
                        Instantiate(enemyAlyuvarForAtackCharacter, enemyPosition, Quaternion.identity);
                    }
                    


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

            Debug.Log("Renk Deðiþiyor");

            if (virusMaterial.color == virusMaterialColors[targetVirusMaterialColorsNumber])
            {
                nowVirusMaterialColorsNumber = targetVirusMaterialColorsNumber;
                changeColorCompleteControl = true;
                Debug.Log("Hedef Renge Ulaþýldý");
                break;
            }
        }

    }


}
