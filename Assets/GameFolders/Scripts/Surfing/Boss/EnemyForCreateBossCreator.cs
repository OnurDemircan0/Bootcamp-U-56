using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForCreateBossCreator : MonoBehaviour
{
    [SerializeField] private GameObject enemyForCreateBoss;


    [SerializeField] private Transform characterTransform;
    [SerializeField] private Transform bossTransform;

    [SerializeField] private float enemyMinFarFromCharacter;
    [SerializeField] private float enemyMinFarFromBoss;

    [SerializeField] private float enemyMaxFarFromCenterOfVein;

    [SerializeField] private float minEnemyCountFrontOfCharacter;
    [SerializeField] private float maxEnemyCountFrontOfCharacter;

    [SerializeField] private float minSpeedForGoBoss;
    [SerializeField] private float maxSpeedForGoBoss;

    private float enemyFrontOfCharacterCount;

    private Vector3 enemyPosition;

    void Awake()
    {
        enemyFrontOfCharacterCount = Random.Range(minEnemyCountFrontOfCharacter, maxEnemyCountFrontOfCharacter);


        createEnemies();
    }

    private void createEnemies()
    {
        for(int i =0; i< enemyFrontOfCharacterCount; i++)
        {
            enemyPosition = new Vector3(Random.Range(enemyMaxFarFromCenterOfVein * -1, enemyMaxFarFromCenterOfVein), 
                Random.Range(enemyMaxFarFromCenterOfVein * -1, enemyMaxFarFromCenterOfVein), 
                Random.Range(characterTransform.transform.position.z + enemyMinFarFromCharacter, bossTransform.transform.position.z - enemyMinFarFromBoss));

            //GoToDifferentObject goToDifferentObject = Instantiate(enemyForCreateBoss, enemyPosition, Quaternion.identity).GetComponent<GoToDifferentObject>();
            GoToDifferentObject goToDifferentObject = Instantiate(enemyForCreateBoss, enemyPosition, 
                Quaternion.Euler(new Vector3(Random.Range(0,359), Random.Range(0, 359), Random.Range(0, 359)))).GetComponent<GoToDifferentObject>();

            goToDifferentObject.minSpeed = minSpeedForGoBoss;
            goToDifferentObject.maxSpeed = maxSpeedForGoBoss;

        }


    }
}
