using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfingController : MonoBehaviour
{

    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    public float alyuvarRotationMaxiumValueX = 15.0f;
    public float alyuvarRotationMaxiumValueY = 15.0f;


    private float startRotationValueX;
    private float startRotationValueY;

    float translationX;
    float translationZ;

    float rotationX;
    float rotationY;

    float inputVertical;
    float inputHorizontal;

    private float nowRotationValueX;
    private float nowRotationValueY;


    void Start()
    {
        startRotationValueX = gameObject.transform.localRotation.eulerAngles.x;
        startRotationValueY = gameObject.transform.localRotation.eulerAngles.y;

        Debug.Log("startRotationValueX: " + startRotationValueX);
        Debug.Log("startRotationValueY: " + startRotationValueY);

    }


    void Update()
    {
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");



        translationX = inputVertical * speed * Time.deltaTime;
        translationZ = inputHorizontal * speed * Time.deltaTime * - 1;

        //Debug.Log("translation X: " + translationX * alyuvarRotationMaxiumValueX);
        //Debug.Log("translation Z: " + translationZ * alyuvarRotationMaxiumValueY);

        transform.Translate(translationX, 0, translationZ, Space.World);



        rotationY = inputVertical * rotationSpeed * Time.deltaTime;
        rotationX = inputHorizontal * rotationSpeed * Time.deltaTime;


        
        nowRotationValueX = gameObject.transform.localRotation.eulerAngles.x;
        nowRotationValueY = gameObject.transform.localRotation.eulerAngles.y;
        //Debug.Log("nowRotationValueX: " + nowRotationValueX);
        //Debug.Log("nowRotationValueY: " + nowRotationValueY);        

        rotateAlyuvar();


        //Debug.Log("rotationX: " + rotationX);
        //Debug.Log("rotationY: " + rotationY);

        //transform.Rotate(rotationX, rotationY, 0);

        if (nowRotationValueX > startRotationValueX - alyuvarRotationMaxiumValueX && nowRotationValueX < startRotationValueX + alyuvarRotationMaxiumValueX)
        {
            //Debug.Log("Döndürülüyor...");
            //transform.Rotate(rotationX, 0, 0);

        }

        if (nowRotationValueX > startRotationValueX - alyuvarRotationMaxiumValueX && nowRotationValueX < startRotationValueX + alyuvarRotationMaxiumValueX)
        {
            //Debug.Log("Döndürülüyor...");
            //transform.Rotate(rotationX, 0, 0);

        }


        // Get the mouse delta. This is not in the range -1...1
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        //transform.Rotate(v, h, 0);



    }


    private void rotateAlyuvar() 
    {
        if (nowRotationValueX <= startRotationValueX + alyuvarRotationMaxiumValueX && nowRotationValueX >= startRotationValueX - alyuvarRotationMaxiumValueX)
        {
            Debug.Log("Döndürülüyor");
            transform.Rotate(0, rotationY, 0);
        }

        /*
        if (nowRotationValueX <= startRotationValueX + alyuvarRotationMaxiumValueX && inputVertical > 0)
        {
            Debug.Log("Döndürülüyor 1");
            transform.Rotate(0, rotationY, 0);
        }
        else if (nowRotationValueX >= startRotationValueX - alyuvarRotationMaxiumValueX && inputVertical < 0)
        {
            Debug.Log("Döndürülüyor 2");
            transform.Rotate(0, rotationY, 0);
        }
        */
    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = gameObject.transform;
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Temastan Çýktý");

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
