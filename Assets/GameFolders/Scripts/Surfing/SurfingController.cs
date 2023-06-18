using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation;

public class SurfingController : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    float dstTravelled;

    [SerializeField] private float startPoint;



    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    public float heightSpeed = 3.0f;
    public float alyuvarRotationMaxiumValueX = 15.0f;
    public float alyuvarRotationMaxiumValueY = 15.0f;

    public float maxDistanceFromPathPoint = 1.0f;

    


    private float startRotationValueX;
    private float startRotationValueY;

    float translationX;
    float translationZ;

    float xDistanceFromPathPoint1 = 0.0f;
    float xDistanceFromPathPoint2 = 0.0f;
    float yDistanceFromPathPoint1 = 0.0f;
    float yDistanceFromPathPoint2 = 0.0f;

    float rotationX;
    float rotationY;

    float inputVertical;
    float inputHorizontal;

    

    private float nowRotationValueX;
    private float nowRotationValueY;

    private int nearestPointNumberForCharacter; // Karektere en yakýn noktanýn indeksi
    private Vector3 nearestPointForCharacter; // Karektere en yakýn noktanýn transformu
    private Vector3 point1; // Karektere en yakýn noktayý bulmak için kullanýlacak deðiþken
    private Vector3 point2; // Karektere en yakýn noktayý bulmak için kullanýlacak deðiþken
    private float point1Distance; // Point 1 in karektere olan uzaklýðý
    private float point2Distance; // Point 2 in karektere olan uzaklýðý


    void Start()
    {
        startRotationValueX = gameObject.transform.localRotation.eulerAngles.x;
        startRotationValueY = gameObject.transform.localRotation.eulerAngles.y;

        Debug.Log("startRotationValueX: " + startRotationValueX);
        Debug.Log("startRotationValueY: " + startRotationValueY);


        dstTravelled = startPoint;

        //transform.position = pathCreator.path.GetPoint(0);

        nearestPointNumberForCharacter = 0;
        nearestPointForCharacter = pathCreator.path.localPoints[nearestPointNumberForCharacter];

        /*
        foreach (var transformpoints in pathCreator.path.localPoints)
        {
            Debug.Log("Noktalar: " + transformpoints);
        }
        */

    }


    void Update()
    {
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");

        // "q" tuþuna basýlýrsa yüksekliði artýr
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + heightSpeed * Time.deltaTime, transform.position.z);
        }

        // "e" tuþuna basýlýrsa yüksekliði azalt
        if (Input.GetKey(KeyCode.E))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - heightSpeed * Time.deltaTime, transform.position.z);
        }



        translationX = inputVertical * speed * Time.deltaTime;
        translationZ = inputHorizontal * speed * Time.deltaTime;

        //Debug.Log("translation X: " + translationX * alyuvarRotationMaxiumValueX);
        //Debug.Log("translation Z: " + translationZ * alyuvarRotationMaxiumValueY);

        //transform.Translate(translationX, 0, translationZ, Space.World);
        transform.Translate(translationZ, 0, translationX, Space.World);



        rotationY = inputVertical * rotationSpeed * Time.deltaTime;
        rotationX = inputHorizontal * rotationSpeed * Time.deltaTime;


        
        nowRotationValueX = gameObject.transform.localRotation.eulerAngles.x;
        nowRotationValueY = gameObject.transform.localRotation.eulerAngles.y;
        //Debug.Log("nowRotationValueX: " + nowRotationValueX);
        //Debug.Log("nowRotationValueY: " + nowRotationValueY);        

        //rotateAlyuvar();


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

        if(gameObject.transform.childCount > 0)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition 
                | RigidbodyConstraints.FreezeRotation;

            followPath();
        }
        
        //blockFarFromPath();



    }

    private void followPath()
    {
        dstTravelled += speed * Time.deltaTime;
        //transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);

        xDistanceFromPathPoint1 = inputHorizontal * maxDistanceFromPathPoint * Time.deltaTime * 10;

        if (xDistanceFromPathPoint1 != xDistanceFromPathPoint2)
        {
            xDistanceFromPathPoint2 += xDistanceFromPathPoint1;
        }

        if(xDistanceFromPathPoint2 > maxDistanceFromPathPoint)
        {
            xDistanceFromPathPoint2 = maxDistanceFromPathPoint;
        }
        else if (xDistanceFromPathPoint2 < maxDistanceFromPathPoint * -1)
        {
            xDistanceFromPathPoint2 = maxDistanceFromPathPoint * -1;
        }



        yDistanceFromPathPoint1 = inputVertical * maxDistanceFromPathPoint * Time.deltaTime * 10;

        if (yDistanceFromPathPoint1 != yDistanceFromPathPoint2)
        {
            yDistanceFromPathPoint2 += yDistanceFromPathPoint1;
        }

        if (yDistanceFromPathPoint2 > maxDistanceFromPathPoint)
        {
            yDistanceFromPathPoint2 = maxDistanceFromPathPoint;
        }
        else if (yDistanceFromPathPoint2 < maxDistanceFromPathPoint * -1)
        {
            yDistanceFromPathPoint2 = maxDistanceFromPathPoint * -1;
        }

        Debug.Log("xDistanceFromPathPoint1: " + xDistanceFromPathPoint1);
        Debug.Log("xDistanceFromPathPoint2: " + xDistanceFromPathPoint2);

        Debug.Log("yDistanceFromPathPoint1: " + yDistanceFromPathPoint1);
        Debug.Log("yDistanceFromPathPoint2: " + yDistanceFromPathPoint2);




        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + xDistanceFromPathPoint2
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + yDistanceFromPathPoint2
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z);

        /*
        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + inputHorizontal * maxDistanceFromPathPoint
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + inputVertical * maxDistanceFromPathPoint
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z);
        */


        Debug.Log("Character Rotatinon: " + transform.rotation.eulerAngles);
        Debug.Log("Rotatinon: " + pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles);
        //transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);

        //transform.rotation = Quaternion.Euler(new Vector3(270,0,0));
        transform.rotation = Quaternion.Euler(new Vector3(pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.x + 270
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.y
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.z - 270));
    }


    private void blockFarFromPath()
    {
        point1 = pathCreator.path.localPoints[nearestPointNumberForCharacter];
        point2 = pathCreator.path.localPoints[nearestPointNumberForCharacter + 1];

        point1Distance = Vector3.Distance(gameObject.transform.position, point1);
        point2Distance = Vector3.Distance(gameObject.transform.position, point2);

        //Debug.Log("point1Distance: " + point1Distance);
        //Debug.Log("point2Distance: " + point2Distance);

        if(point1Distance < point2Distance)
        {
            nearestPointForCharacter = point1;
        }
        else
        {
            nearestPointForCharacter = point2;
            nearestPointNumberForCharacter++;
        }


        Debug.Log("En Yakýn Nokta: " + nearestPointForCharacter);

        if(transform.position.x > nearestPointForCharacter.x + maxDistanceFromPathPoint)
        {
            transform.position = new Vector3(nearestPointForCharacter.x + maxDistanceFromPathPoint, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < nearestPointForCharacter.x - maxDistanceFromPathPoint)
        {
            transform.position = new Vector3(nearestPointForCharacter.x - maxDistanceFromPathPoint, transform.position.y, transform.position.z);
        }

        if (transform.position.y > nearestPointForCharacter.y + maxDistanceFromPathPoint)
        {
            transform.position = new Vector3(transform.position.x, nearestPointForCharacter.y + maxDistanceFromPathPoint, transform.position.z);
        }
        else if (transform.position.y < nearestPointForCharacter.y - maxDistanceFromPathPoint)
        {
            transform.position = new Vector3(transform.position.x, nearestPointForCharacter.y - maxDistanceFromPathPoint, transform.position.z);
        }

    }


    private void rotateAlyuvar() 
    {
        // Saða sola dönme
        //transform.Rotate(Vector3.up, inputHorizontal * rotationSpeed * Time.deltaTime);

        // Ýleri geri dönme (opsiyonel)
        //transform.Rotate(Vector3.right, inputVertical * rotationSpeed * Time.deltaTime);


        if ((nowRotationValueX <= startRotationValueX + alyuvarRotationMaxiumValueX 
            || (nowRotationValueX >= startRotationValueX + alyuvarRotationMaxiumValueX && inputHorizontal < 0)))
        {
            //Debug.Log("Döndürülüyor 3");
            //transform.Rotate(Vector3.up, inputHorizontal * rotationSpeed * Time.deltaTime);
        }

        if (nowRotationValueX > startRotationValueX - alyuvarRotationMaxiumValueX
            || (nowRotationValueX <= startRotationValueX - alyuvarRotationMaxiumValueX && inputHorizontal > 0))
        {
            Debug.Log("Döndürülüyor 4");
            transform.Rotate(Vector3.up, inputHorizontal * rotationSpeed * Time.deltaTime);
        }


        if (nowRotationValueX <= startRotationValueX + alyuvarRotationMaxiumValueX || inputVertical > 0)
        {
            //Debug.Log("Döndürülüyor");
            //transform.Rotate(rotationY, 0, 0);
        }

        if (nowRotationValueX >= startRotationValueX - alyuvarRotationMaxiumValueX || inputVertical > 0)
        {
            //Debug.Log("Döndürülüyor 2");
            //transform.Rotate(rotationY, 0, 0);
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
