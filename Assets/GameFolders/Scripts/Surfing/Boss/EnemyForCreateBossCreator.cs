using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForCreateBossCreator : MonoBehaviour
{
    [SerializeField] private GameObject enemyForCreateBoss;
    [SerializeField] private SurfingControllerV2 surfingControllerV2;


    [SerializeField] private Transform characterTransform;
    [SerializeField] private Transform bossTransform;

    [SerializeField] private float enemyMinFarFromCharacter;
    [SerializeField] private float enemyMinFarFromBoss;

    [SerializeField] private float enemyMinFarFromCharacterX;
    [SerializeField] private float enemyMinFarFromBossX;
    [SerializeField] private float enemyMinFarFromCharacterZ;
    [SerializeField] private float enemyMinFarFromBossZ;

    [SerializeField] private float enemyMaxFarFromCenterOfVein;

    [SerializeField] private float minEnemyCountFrontOfCharacter;
    [SerializeField] private float maxEnemyCountFrontOfCharacter;

    [SerializeField] private float minSpeedForGoBoss;
    [SerializeField] private float maxSpeedForGoBoss;

    private float enemyRandomPositionX;
    private float enemyRandomPositionY;
    private float enemyRandomPositionZ;

    private float enemyFrontOfCharacterCount;

    private bool enemyCreatedControl = false;

    private Vector3 enemyPosition;

    void Awake()
    {
        enemyFrontOfCharacterCount = Random.Range(minEnemyCountFrontOfCharacter, maxEnemyCountFrontOfCharacter);


        //createEnemies();
    }

    private void Update()
    {
        if(enemyCreatedControl == false)
        {
            if (surfingControllerV2.contactBossControl)
            {
                createEnemies();
                enemyCreatedControl = true;


                Debug.Log("characterTransform.transform.position.x: " + characterTransform.transform.position.x);
                Debug.Log("bossTransform.transform.position.x: " + bossTransform.transform.position.x);
            }
        }
    }

    private void createEnemies()
    {
        for(int i =0; i< enemyFrontOfCharacterCount; i++)
        {
            /*
            enemyPosition = new Vector3(Random.Range(enemyMaxFarFromCenterOfVein * -1, enemyMaxFarFromCenterOfVein), 
                Random.Range(enemyMaxFarFromCenterOfVein * -1, enemyMaxFarFromCenterOfVein), 
                Random.Range(characterTransform.transform.position.z + enemyMinFarFromCharacter, bossTransform.transform.position.z - enemyMinFarFromBoss));
            */

            /*
            // Virüs ve Karekter Arasýndaki Noktalara Göre Hesapladýðým Doðru Formülü -> X = Z * 1.76
            enemyRandomPositionX = Random.Range(characterTransform.transform.position.x + enemyMinFarFromCharacterX, bossTransform.transform.position.x - enemyMinFarFromBossX);
            enemyRandomPositionZ = enemyRandomPositionX / 1.76f + characterTransform.transform.position.z - 50;

            Debug.Log("enemyRandomPositionX: " + enemyRandomPositionX);
            Debug.Log("enemyRandomPositionZ: " + enemyRandomPositionZ);

            enemyPosition = new Vector3(enemyRandomPositionX, Random.Range(enemyMaxFarFromCenterOfVein * -1, enemyMaxFarFromCenterOfVein), enemyRandomPositionZ);
            */

            // Virüs ve Karekter Arasýndaki Noktalara Göre Hesapladýðým Doðru Formülü -> z - 3340 = 0.5839(x - 250)
            enemyRandomPositionX = Random.Range(235, 360);
            //enemyRandomPositionZ = ((enemyRandomPositionX - 250) * 0.5839f) + 3340;
            enemyRandomPositionZ = ((enemyRandomPositionX - 250) * 0.5839f) + Random.Range(3333f,3347f);


            enemyPosition = new Vector3(enemyRandomPositionX, Random.Range(enemyMaxFarFromCenterOfVein * -1, enemyMaxFarFromCenterOfVein), enemyRandomPositionZ);

            //GoToDifferentObject goToDifferentObject = Instantiate(enemyForCreateBoss, enemyPosition, Quaternion.identity).GetComponent<GoToDifferentObject>();
            GoToDifferentObject goToDifferentObject = Instantiate(enemyForCreateBoss, enemyPosition, 
                Quaternion.Euler(new Vector3(Random.Range(0,359), Random.Range(0, 359), Random.Range(0, 359)))).GetComponent<GoToDifferentObject>();

            goToDifferentObject.minSpeed = minSpeedForGoBoss;
            goToDifferentObject.maxSpeed = maxSpeedForGoBoss;

        }


    }
}
