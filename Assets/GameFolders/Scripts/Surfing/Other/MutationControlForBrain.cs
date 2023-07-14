using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationControlForBrain : MonoBehaviour
{
    private VirusController virusController;
    private EnemyInVeinController enemyInVeinController;

    [SerializeField] private bool randomMutationControl;

    [SerializeField] private bool mutationDivideActiveControl;
    [SerializeField] private bool changeColorActiveControl;
    [SerializeField] private bool changeSizeActiveControl;

    [SerializeField] private bool mutationDivideControl;
    [SerializeField] private bool changeColorControl;
    [SerializeField] private bool changeSizeControl;


    

    [SerializeField] private bool changeColorCompleteControl;
    [SerializeField] private bool changeSizeCompleteControl;
    [SerializeField] private bool mutationDivideCompleteControl;

    //private bool wantedScaleChangedControl; //Enemy yi küçültmek için kullanýlan deðeri eðer enemy mutasyon geçirip büyüdüyse düzeltilip düzeltilmediðini kontrol eder


    [SerializeField] private float minSpeedFarFromDivededViruses;
    [SerializeField] private float maxSpeedFarFromDivededViruses;
    private float speedFarFromDivededViruses;

    [SerializeField] private float minFarValueFromDivededVirusesX;
    [SerializeField] private float maxFarValueFromDivededVirusesX;
    //[SerializeField] private float minFarValueFromDivededVirusesY;
    //[SerializeField] private float maxFarValueFromDivededVirusesY;
    //[SerializeField] private float minFarValueFromDivededVirusesZ;
    //[SerializeField] private float maxFarValueFromDivededVirusesZ;
    private float maxFarValueX;
    private float maxFarValueY;
    private float maxFarValueZ;


    [Range(0.1f, 10f)] [SerializeField] private float minChangeColorSpeed;
    [Range(0.1f, 10f)] [SerializeField] private float maxChangeColorSpeed;
    private float changeColorSpeed;
    public Color[] virusMaterialNormalColors;
    public Color[] virusMaterialNormalColorsEmission;
    public Color[] virusMaterialLightColors;
    public Color[] virusMaterialLightColorsEmission;
    private Material virusMaterial1;
    private Material virusMaterial2;
    public int nowVirusMaterialColorsNumber = -1;
    public int targetVirusMaterialColorsNumber = -1;

    [Range(100f, 2000f)] [SerializeField] private float minChangeSizeSpeed;
    [Range(100f, 2000f)] [SerializeField] private float maxChangeSizeSpeed;
    private float changeSizeSpeed;
    [SerializeField] private float minChangeSizeValue;
    [SerializeField] private float maxChangeSizeValue;
    private float changeSizeValue;
    private float firstSizeValue;


    private float nowFarValueX = 0.0f;
    private float nowFarValueY = 0.0f;
    private float nowFarValueZ = 0.0f;
    private float nowSumFarValueX = 0.0f;
    private float nowSumFarValueY = 0.0f;
    private float nowSumFarValueZ = 0.0f;

    private bool beforeMutated = false;

    [SerializeField] private int minRanmdomMutationNumberForDetermineMutations;
    [SerializeField] private int maxRanmdomMutationNumberForDetermineMutations;
    private int ranmdomMutationNumberForDetermineMutations;

    private GameObject newDividedVirus;

    private void Awake()
    {

        //enemyInVeinController = gameObject.GetComponent<EnemyInVeinController>();

        //virusController = gameObject.GetComponent<VirusController>();

        virusMaterial1 = gameObject.GetComponent<Renderer>().materials[0];
        virusMaterial2 = gameObject.GetComponent<Renderer>().materials[1];
    }

    void Start()
    {
        mutationDivideControl = false;
        mutationDivideCompleteControl = true;

        changeColorControl = false;
        changeColorCompleteControl = true;

        changeSizeControl = false;
        changeSizeCompleteControl = true;
        

        if (nowVirusMaterialColorsNumber == -1)
        {
            nowVirusMaterialColorsNumber = Random.Range(0, virusMaterialNormalColors.Length);
        }

        virusMaterial1.color = virusMaterialNormalColors[nowVirusMaterialColorsNumber];
        virusMaterial1.SetColor("_EmissionColor", virusMaterialNormalColorsEmission[nowVirusMaterialColorsNumber]);
        virusMaterial2.color = virusMaterialLightColors[nowVirusMaterialColorsNumber];
        virusMaterial2.SetColor("_EmissionColor", virusMaterialLightColorsEmission[nowVirusMaterialColorsNumber]);


        targetVirusMaterialColorsNumber = nowVirusMaterialColorsNumber;


        firstSizeValue = transform.localScale.x;

    }

    public void mutateVirus()
    {
        //Kendi kendinede mutayon geçirsin bazen

        //Mutosyon 1 -> Rengini deðiþtir KÜÇÜK MUTASYON
        //Mutosyon 2 -> Boyutunu deðiþtir KÜÇÜK MUTASYON
        //Mutosyon 3 -> Bölünme gerçekleþtir KÜÇÜK MUTASYON

        //Mutosyon 4 -> Hem rengini hem de boyutunu deðiþtir BÜYÜk MUTASYON
        //Mutosyon 5 -> Hem renk deðiþtir hem de Bölünme gerçekleþtir BÜYÜk MUTASYON
        //Mutosyon 6 -> Hem Boyutunu deðiþtir hem de Bölünme gerçekleþtir BÜYÜk MUTASYON

        //Mutosyon 7 -> Hem rengini hem de boyutunu deðiþtir hem de Bölünme gerçekleþtir ULTRA MUTASYON
        //Mutosyon 8 -> 4 - 8 - 16 Bölünme gerçekleþtir ULTRA MUTASYON

        //Mutosyon 9 -> Etrafýnda Kalkan Oluþsun Diðer mutasyonlardan farklý random fankisyonuna koy


        ranmdomMutationNumberForDetermineMutations = Random.Range(minRanmdomMutationNumberForDetermineMutations, maxRanmdomMutationNumberForDetermineMutations);

        switch (ranmdomMutationNumberForDetermineMutations)
        {
            case 0:
                changeColorControl = true;
                break;
            case 1:
                changeSizeControl = true;
                break;
            case 2:
                mutationDivideControl = true;
                break;
            case 3:
                changeColorControl = true;
                changeSizeControl = true;
                break;
            case 4:
                changeColorControl = true;
                mutationDivideControl = true;
                break;
            case 5:
                changeSizeControl = true;
                mutationDivideControl = true;
                break;
            case 6:
                changeColorControl = true;
                changeSizeControl = true;
                mutationDivideControl = true;
                break;

            default:
                changeColorControl = true;
                changeSizeControl = true;
                mutationDivideControl = true;
                break;
        }

        
        if(changeColorActiveControl == false)
        {
            changeColorControl = false;
        }

        if (changeSizeActiveControl == false)
        {
            changeSizeControl = false;
        }

        if (mutationDivideActiveControl == false)
        {
            mutationDivideControl = false;
        }


        if (changeColorActiveControl == false && changeSizeActiveControl == false && mutationDivideActiveControl == false)
        {
            Debug.Log("Tüm mutasyonlar kapalý");
        }
        else
        {
            if (changeColorControl == false && changeSizeControl == false && mutationDivideControl == false)
            {
                Debug.Log("Mutasyon fonksiyonu tekrardan çalýþtý");
                mutateVirus();
            }
        }
        

        
        /*
        if(beforeMutated == false)
        {
            //Eðer önceden hiç mutasyon geçirmediyse mutasyon geçirsin

            beforeMutated = true;
        }
        */
    }



    IEnumerator changeColorVirus()
    {
        changeColorCompleteControl = false;

        changeColorSpeed = Random.Range(minChangeColorSpeed, maxChangeColorSpeed);

        targetVirusMaterialColorsNumber = Random.Range(0, virusMaterialNormalColors.Length);
        while (targetVirusMaterialColorsNumber == nowVirusMaterialColorsNumber)
        {
            targetVirusMaterialColorsNumber = Random.Range(0, virusMaterialNormalColors.Length);
        }

        //virusMaterial1.SetColor("_EmissionColor", virusMaterialNormalColorsEmission[targetVirusMaterialColorsNumber]);
        //virusMaterial2.SetColor("_EmissionColor", virusMaterialLightColorsEmission[targetVirusMaterialColorsNumber]);

        /*
        virusMaterial1.color = virusMaterialNormalColors[targetVirusMaterialColorsNumber];
        virusMaterial1.SetColor("_EmissionColor", virusMaterialNormalColorsEmission[targetVirusMaterialColorsNumber]);
        virusMaterial2.color = virusMaterialLightColors[targetVirusMaterialColorsNumber];
        virusMaterial2.SetColor("_EmissionColor", virusMaterialLightColorsEmission[targetVirusMaterialColorsNumber]);
        */

        while (true)
        {
            //virusMaterial.color = Color.Lerp(virusMaterial.color, virusMaterialColors[2], 0.01f);
            //virusMaterial.color = Color.Lerp(virusMaterial.color, virusMaterialColors[targetVirusMaterialColorsNumber], changeColorSpeed / 100);

            virusMaterial1.color = Color.Lerp(virusMaterial1.color, virusMaterialNormalColors[targetVirusMaterialColorsNumber], changeColorSpeed / 100);
            virusMaterial1.SetColor("_EmissionColor", Color.Lerp(virusMaterial1.color, virusMaterialNormalColorsEmission[targetVirusMaterialColorsNumber], changeColorSpeed / 100));
            virusMaterial2.color = Color.Lerp(virusMaterial2.color, virusMaterialLightColors[targetVirusMaterialColorsNumber], changeColorSpeed / 100);
            virusMaterial2.SetColor("_EmissionColor", Color.Lerp(virusMaterial2.color, virusMaterialLightColorsEmission[targetVirusMaterialColorsNumber], changeColorSpeed / 100));
           

            yield return null;

            //Debug.Log("Renk Deðiþiyor");

            /*
            if (virusMaterial1.color == virusMaterialNormalColors[targetVirusMaterialColorsNumber] )
            {
                nowVirusMaterialColorsNumber = targetVirusMaterialColorsNumber;
                changeColorCompleteControl = true;
                //Debug.Log("Hedef Renge Ulaþýldý");
                break;
            }
            */

            /*
            if (virusMaterial1.color == virusMaterialNormalColors[targetVirusMaterialColorsNumber])
            {
                nowVirusMaterialColorsNumber = targetVirusMaterialColorsNumber;
                changeColorCompleteControl = true;
                //Debug.Log("Hedef Renge Ulaþýldý");

                virusMaterial1.color = virusMaterialNormalColors[targetVirusMaterialColorsNumber];
                virusMaterial1.SetColor("_EmissionColor", virusMaterialNormalColorsEmission[targetVirusMaterialColorsNumber]);
                virusMaterial2.color = virusMaterialLightColors[targetVirusMaterialColorsNumber];
                virusMaterial2.SetColor("_EmissionColor", virusMaterialLightColorsEmission[targetVirusMaterialColorsNumber]);


                break;
            }
            */

            if (virusMaterial1.color == virusMaterialNormalColors[targetVirusMaterialColorsNumber] &&
                virusMaterial2.color == virusMaterialLightColors[targetVirusMaterialColorsNumber])
            {
                nowVirusMaterialColorsNumber = targetVirusMaterialColorsNumber;
                changeColorCompleteControl = true;
                //Debug.Log("Hedef Renge Ulaþýldý");

                virusMaterial1.color = virusMaterialNormalColors[targetVirusMaterialColorsNumber];
                virusMaterial1.SetColor("_EmissionColor", virusMaterialNormalColorsEmission[targetVirusMaterialColorsNumber]);
                virusMaterial2.color = virusMaterialLightColors[targetVirusMaterialColorsNumber];
                virusMaterial2.SetColor("_EmissionColor", virusMaterialLightColorsEmission[targetVirusMaterialColorsNumber]);


                break;
            }


            /*
            if (virusMaterial1.color == virusMaterialNormalColors[targetVirusMaterialColorsNumber] &&
                virusMaterial1.GetColor("_EmissionColor") == virusMaterialNormalColorsEmission[targetVirusMaterialColorsNumber] &&
                virusMaterial2.color == virusMaterialLightColors[targetVirusMaterialColorsNumber] &&
                virusMaterial2.GetColor("_EmissionColor") == virusMaterialLightColorsEmission[targetVirusMaterialColorsNumber])
            {
                nowVirusMaterialColorsNumber = targetVirusMaterialColorsNumber;
                changeColorCompleteControl = true;
                //Debug.Log("Hedef Renge Ulaþýldý");
                break;
            }
            */
        }

    }

    IEnumerator changeScaleVirus()
    {
        changeSizeCompleteControl = false;

        changeSizeSpeed = Random.Range(minChangeSizeSpeed, maxChangeSizeSpeed);
        changeSizeValue = Random.Range(minChangeSizeValue, maxChangeSizeValue);

        //enemyInVeinController.nowScale = firstSizeValue + changeSizeValue;
        //enemyInVeinController.wantedScale = firstSizeValue + changeSizeValue;


        Debug.Log("Boyut Deðiþmeye Baþladý");

        while (true)
        {
            yield return null;

            Debug.Log("Boyut Deðiþiyor");

            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * changeSizeSpeed * Time.deltaTime;

            if (transform.localScale.x >= firstSizeValue + changeSizeValue)
            {
                changeSizeCompleteControl = true;
                Debug.Log("Hedef Boyuta Ulaþýldý");
                break;
            }
        }
    }

    private void divedeVirus()
    {
        newDividedVirus = Instantiate(this.transform.parent).gameObject;
        //newDividedVirus = Instantiate(this.transform.parent).gameObject;
        //newDividedVirus.GetComponent<MutationControlForBrain>().nowVirusMaterialColorsNumber = nowVirusMaterialColorsNumber;
    }

    IEnumerator changePositionDividedViruses()
    {
        mutationDivideCompleteControl = false;

        maxFarValueX = Random.Range(minFarValueFromDivededVirusesX + transform.parent.localScale.x, maxFarValueFromDivededVirusesX + transform.parent.localScale.x);
        //maxFarValueY = Random.Range(minFarValueFromDivededVirusesY + transform.localScale.x, maxFarValueFromDivededVirusesY + transform.localScale.x);
        //maxFarValueZ = Random.Range(minFarValueFromDivededVirusesZ + transform.localScale.x, maxFarValueFromDivededVirusesZ + transform.localScale.x);

        maxFarValueY = maxFarValueX;
        maxFarValueZ = maxFarValueX;


        Debug.Log("maxFarValueX: " + maxFarValueX);

        speedFarFromDivededViruses = Random.Range(minSpeedFarFromDivededViruses, maxSpeedFarFromDivededViruses);

        int directOfTransformX = Random.Range(0, 2);
        int directOfTransformY = Random.Range(0, 2);
        int directOfTransformZ = Random.Range(0, 2);

        while (true)
        {
            //yield return new WaitForEndOfFrame();
            yield return null;

            nowFarValueX = speedFarFromDivededViruses * Time.deltaTime;
            nowFarValueY = speedFarFromDivededViruses * Time.deltaTime;
            nowFarValueZ = speedFarFromDivededViruses * Time.deltaTime;

            nowSumFarValueX += nowFarValueX * 2; // 2 ile çarpýlýyor çünkü iki virus de hareket ediyor
            nowSumFarValueY += nowFarValueY * 2; // 2 ile çarpýlýyor çünkü iki virus de hareket ediyor
            nowSumFarValueZ += nowFarValueZ * 2; // 2 ile çarpýlýyor çünkü iki virus de hareket ediyor

            //Debug.Log("nowSumFarValueX: " + nowSumFarValueX.ToString());
            //Debug.Log("nowSumFarValueY: " + nowSumFarValueY.ToString());
            //Debug.Log("nowSumFarValueZ: " + nowSumFarValueZ.ToString());

            /*
            virusController.nowPositionX = nowSumFarValueX;
            virusController.nowPositionY = nowSumFarValueY;
            virusController.nowPositionZ = nowSumFarValueZ;
            */


            if (nowSumFarValueX < maxFarValueX)
            {
                if (directOfTransformX == 1)
                {
                    transform.parent.position += new Vector3(nowFarValueX, 0, 0);
                    newDividedVirus.transform.position += new Vector3(nowFarValueX * -1, 0, 0);

                }
                else
                {
                    transform.parent.position += new Vector3(nowFarValueX * -1, 0, 0);
                    newDividedVirus.transform.position += new Vector3(nowFarValueX, 0, 0);
                }

            }

            /*
            if (nowSumFarValueY < maxFarValueY)
            {
                if (directOfTransformY == 1)
                {
                    transform.parent.position += new Vector3(0, nowFarValueY, 0);
                    newDividedVirus.transform.position += new Vector3(0, nowFarValueY * -1, 0);
                }
                else
                {
                    transform.parent.position += new Vector3(0, nowFarValueY * -1, 0);
                    newDividedVirus.transform.position += new Vector3(0, nowFarValueY, 0);
                }

            }
            */

            if (nowSumFarValueZ < maxFarValueZ)
            {
                if (directOfTransformZ == 1)
                {
                    transform.parent.position += new Vector3(0, 0, nowFarValueZ);
                    newDividedVirus.transform.position += new Vector3(0, 0, nowFarValueZ * -1);
                }
                else
                {
                    transform.parent.position += new Vector3(0, 0, nowFarValueZ * -1);
                    newDividedVirus.transform.position += new Vector3(0, 0, nowFarValueZ);
                }

            }


            //virusController.nowPositionX = transform.position.x;
            //virusController.nowPositionY = transform.position.y;
            //virusController.nowPositionZ = transform.position.z;



            /*
            virusController.addPositionX = directOfTransformX == 1 ? nowSumFarValueX / 2 : nowSumFarValueX / 2 * -1;
            virusController.addPositionY = directOfTransformY == 1 ? nowSumFarValueY / 2 : nowSumFarValueY / 2 * -1;


            newDividedVirus.GetComponent<VirusController>().addPositionX = directOfTransformX == 1 ? nowSumFarValueX / 2 * -1 : nowSumFarValueX / 2;
            newDividedVirus.GetComponent<VirusController>().addPositionY = directOfTransformY == 1 ? nowSumFarValueY / 2 * -1 : nowSumFarValueY / 2;
            */


            if (nowSumFarValueX >= maxFarValueX && nowSumFarValueY >= maxFarValueY && nowSumFarValueZ >= maxFarValueZ)
            {

                nowSumFarValueX = 0;
                nowSumFarValueY = 0;
                nowSumFarValueZ = 0;

                mutationDivideCompleteControl = true;
                break;
            }
        }

    }


    private void Update()
    {
        if (mutationDivideControl)
        {
            //Debug.Log("Bölünme Baþladý");

            if (changeColorControl == false && changeSizeControl == false && changeColorCompleteControl == true && changeSizeCompleteControl == true && mutationDivideCompleteControl)
            {
                divedeVirus();
                StartCoroutine(changePositionDividedViruses());

                mutationDivideControl = false;
            }

        }

        if (changeColorControl && changeColorCompleteControl)
        {
            //Debug.Log("Renk deðiþtirme Baþladý");

            StartCoroutine(changeColorVirus());

            changeColorControl = false;
        }

        if (changeSizeControl && changeSizeCompleteControl)
        {
            StartCoroutine(changeScaleVirus());

            changeSizeControl = false;

        }

        if (randomMutationControl)
        {
            mutateVirus();

            randomMutationControl = false;

        }

        


    }
}
