using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StarterAssets;

public class ChangePlayerTransformWithAlyuvar : MonoBehaviour
{
    [SerializeField] private GameObject surfAlyuvarTable;

    [SerializeField] private StarterAssetsInputs starterAssetsInputs;

    private float firstPlayerTransformAccordingToAlyuvarX;
    private float firstPlayerTransformAccordingToAlyuvarY;
    private float firstPlayerTransformAccordingToAlyuvarZ;

    [SerializeField] private float fixedPositionValueY = 0.1f;

    private void Awake()
    {
        determineFirstPlayerTransformAccordingToAlyuvar();
    }


    void Start()
    {
        //Invoke("determineFirstPlayerTransformAccordingToAlyuvar",0.03f);

        //starterAssetsInputs.shoot = true;
    }

    private void blockSomeCharacterProperties()
    {
        starterAssetsInputs.move = new Vector2(0, 0);

        starterAssetsInputs.jump = false;
        starterAssetsInputs.sprint = false;
        starterAssetsInputs.MoveIsPressed = false;
    }

    private void determineFirstPlayerTransformAccordingToAlyuvar()
    {
        

        firstPlayerTransformAccordingToAlyuvarX = surfAlyuvarTable.transform.position.x - transform.position.x;
        firstPlayerTransformAccordingToAlyuvarY = surfAlyuvarTable.transform.position.y - transform.position.y + fixedPositionValueY;
        firstPlayerTransformAccordingToAlyuvarZ = surfAlyuvarTable.transform.position.z - transform.position.z;

        Debug.Log("firstPlayerTransformAccordingToAlyuvarX: " + firstPlayerTransformAccordingToAlyuvarX);
        Debug.Log("firstPlayerTransformAccordingToAlyuvarY: " + firstPlayerTransformAccordingToAlyuvarY);
        Debug.Log("firstPlayerTransformAccordingToAlyuvarZ: " + firstPlayerTransformAccordingToAlyuvarZ);
    }


    void Update()
    {

        transform.position = new Vector3(surfAlyuvarTable.transform.position.x + firstPlayerTransformAccordingToAlyuvarX
            , surfAlyuvarTable.transform.position.y + firstPlayerTransformAccordingToAlyuvarY
            , surfAlyuvarTable.transform.position.z + firstPlayerTransformAccordingToAlyuvarZ);


        blockSomeCharacterProperties();
    }
}
