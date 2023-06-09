using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using PathCreation;

public class SurfingController : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    [SerializeField] private float dstTravelled;

    [SerializeField] private Image imageBlack;



    //[SerializeField] private float startPoint;



    public float speed = 5.0f;
    public float maxSpeed = 50.0f;
    public float speedAcceleration = 0.25f;
    public float rotationSpeed = 100.0f;
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    public float heightSpeed = 3.0f;
    public float alyuvarRotationMaxiumValueX = 15.0f;
    public float alyuvarRotationMaxiumValueY = 15.0f;

    public float maxDistanceFromPathPoint = 1.0f;

    public float turnNormalRotationSpeed = 10.0f;

    public float finishPoint = 1025.0f;
    public float certainFinishPoint = 1065.0f;
    public float blackoutSpeed = 0.1f;

    


    private float startRotationValueX;
    private float startRotationValueY;

    float translationX;
    float translationZ;

    float alyuvarNowRotationValueX = 0.0f;
    float alyuvarNowRotationValueY = 0.0f;
    float alyuvarNowRotationValueZ = 0.0f;

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

    private int nearestPointNumberForCharacter; // Karektere en yak�n noktan�n indeksi
    private Vector3 nearestPointForCharacter; // Karektere en yak�n noktan�n transformu
    private Vector3 point1; // Karektere en yak�n noktay� bulmak i�in kullan�lacak de�i�ken
    private Vector3 point2; // Karektere en yak�n noktay� bulmak i�in kullan�lacak de�i�ken
    private float point1Distance; // Point 1 in karektere olan uzakl���
    private float point2Distance; // Point 2 in karektere olan uzakl���


    void Start()
    {
        startRotationValueX = gameObject.transform.localRotation.eulerAngles.x;
        startRotationValueY = gameObject.transform.localRotation.eulerAngles.y;

        Debug.Log("startRotationValueX: " + startRotationValueX);
        Debug.Log("startRotationValueY: " + startRotationValueY);


        //dstTravelled = startPoint;

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

        // "q" tu�una bas�l�rsa y�ksekli�i art�r
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + heightSpeed * Time.deltaTime, transform.position.z);
        }

        // "e" tu�una bas�l�rsa y�ksekli�i azalt
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
            //Debug.Log("D�nd�r�l�yor...");
            //transform.Rotate(rotationX, 0, 0);


        }

        if (nowRotationValueX > startRotationValueX - alyuvarRotationMaxiumValueX && nowRotationValueX < startRotationValueX + alyuvarRotationMaxiumValueX)
        {
            //Debug.Log("D�nd�r�l�yor...");
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
        if(dstTravelled > 780 && speed < maxSpeed)
        {
            speed += speedAcceleration * Time.deltaTime;
        }
        else if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }

        if(dstTravelled >= finishPoint)
        {
            imageBlack.color = new Color(0, 0, 0, imageBlack.color.a + blackoutSpeed * Time.deltaTime);
        }
        if (dstTravelled >= certainFinishPoint)
        {
            imageBlack.color = new Color(0, 0, 0, 1);
        }

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
        else
        {
            
        }


        yDistanceFromPathPoint1 = inputVertical * maxDistanceFromPathPoint * Time.deltaTime * 10;

        if (yDistanceFromPathPoint1 != yDistanceFromPathPoint2)
        {
            yDistanceFromPathPoint2 += yDistanceFromPathPoint1;
        }

        if (yDistanceFromPathPoint2 > maxDistanceFromPathPoint)
        {
            yDistanceFromPathPoint2 = maxDistanceFromPathPoint;


            rotateAlyuvarNormalRatation();
        }
        else if (yDistanceFromPathPoint2 < maxDistanceFromPathPoint * -1)
        {
            yDistanceFromPathPoint2 = maxDistanceFromPathPoint * -1;


            rotateAlyuvarNormalRatation();
        }
        else
        {
            determineRotateValueOfAlyuvar();
        }

        Debug.Log("xDistanceFromPathPoint1: " + xDistanceFromPathPoint1);
        Debug.Log("xDistanceFromPathPoint2: " + xDistanceFromPathPoint2);

        Debug.Log("yDistanceFromPathPoint1: " + yDistanceFromPathPoint1);
        Debug.Log("yDistanceFromPathPoint2: " + yDistanceFromPathPoint2);



        /*
        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + xDistanceFromPathPoint2
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + yDistanceFromPathPoint2
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z);
        */


        
        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z);

        transform.position += new Vector3(transform.right.x * xDistanceFromPathPoint2,
            transform.right.y * xDistanceFromPathPoint2, 
            transform.right.z * xDistanceFromPathPoint2);

        transform.position += new Vector3(transform.forward.x * yDistanceFromPathPoint2,
            transform.forward.y * yDistanceFromPathPoint2,
            transform.forward.z * yDistanceFromPathPoint2);

        //transform.position += transform.right; 

        //Debug.Log("transform.right: " + transform.right);



        /*
        Vector3 newPosition = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + xDistanceFromPathPoint2
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + yDistanceFromPathPoint2
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z);
        transform.Translate(newPosition * Time.deltaTime, Space.World);
        */

        /*
        transform.Translate(new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + xDistanceFromPathPoint2
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z * -1 * Time.deltaTime
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + yDistanceFromPathPoint2), Space.Self);
        */

        /*
        transform.Translate(new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + xDistanceFromPathPoint2
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z * -1  / speed
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + yDistanceFromPathPoint2) * Time.deltaTime, Space.Self);
        */





        /*
        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + inputHorizontal * maxDistanceFromPathPoint
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + inputVertical * maxDistanceFromPathPoint
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z);
        */


        Debug.Log("Character Rotatinon: " + transform.rotation.eulerAngles);
        Debug.Log("Path Rotatinon: " + pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles);
        //transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);

        //transform.rotation = Quaternion.Euler(new Vector3(270,0,0));
        


        transform.rotation = Quaternion.Euler(new Vector3(pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.x + 270 + alyuvarNowRotationValueX
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.y  + alyuvarNowRotationValueY
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.z - 270 + alyuvarNowRotationValueZ));
        

        /*
        transform.rotation = Quaternion.Euler(new Vector3(pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.x + alyuvarNowRotationValueX
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.y
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.z - 270 + alyuvarNowRotationValueZ));
        */

        //rotateAlyuvarHorizontal();



        //rotateAlyuvarAccordingToInputValue();
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


        Debug.Log("En Yak�n Nokta: " + nearestPointForCharacter);

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

    private void rotateAlyuvarHorizontal()
    {
        transform.Rotate(Vector3.up, inputHorizontal * rotationSpeed * Time.deltaTime);
    }

    private void determineRotateValueOfAlyuvar()
    {
        
        alyuvarNowRotationValueX = inputVertical * alyuvarRotationMaxiumValueX * -1;
        alyuvarNowRotationValueY = 0;
        alyuvarNowRotationValueZ = 0;
        

        /*
        alyuvarNowRotationValueX = inputHorizontal * alyuvarRotationMaxiumValueX;
        alyuvarNowRotationValueY = 0;
        alyuvarNowRotationValueZ = inputVertical * alyuvarRotationMaxiumValueX;
        */


        /*
        if (Math.Abs(inputVertical) >= Math.Abs(inputHorizontal))
        {
            alyuvarNowRotationValueX = inputVertical * alyuvarRotationMaxiumValueX * -1;

            alyuvarNowRotationValueY = 0;
            alyuvarNowRotationValueZ = 0;
        }
        else
        {
            alyuvarNowRotationValueX = inputHorizontal * alyuvarRotationMaxiumValueX * -1;

            //alyuvarNowRotationValueY = 90.0f / (Math.Abs(inputHorizontal) + Math.Abs(inputVertical));
            alyuvarNowRotationValueZ = 0;

            if(Math.Abs(inputHorizontal) > 0.85f && Math.Abs(inputVertical) < 0.15f)
            {
                alyuvarNowRotationValueY = 90.0f;
            }
            else if (Math.Abs(inputHorizontal) < 0.15f && Math.Abs(inputVertical) > 0.85f)
            {
                alyuvarNowRotationValueY = 0.0f;
            }
            else
            {
                alyuvarNowRotationValueY = 45.0f;
            }
        }
        */


        /*
        if(Math.Abs(inputVertical) > 0.95f && Math.Abs(inputHorizontal) < 0.05f)
        {
            alyuvarNowRotationValueY = 0;
            alyuvarNowRotationValueZ = 0;
        }
        else if (Math.Abs(inputHorizontal) > 0.95f && Math.Abs(inputVertical) < 0.05f)
        {
            alyuvarNowRotationValueY = 90;
            alyuvarNowRotationValueZ = -90;
        }
        else
        {
            if(Math.Abs(inputHorizontal) + Math.Abs(inputVertical) != 0)
            {
                alyuvarNowRotationValueY = inputHorizontal > inputVertical ? 90.0f / (Math.Abs(inputHorizontal) + Math.Abs(inputVertical)) 
                    : 90.0f - (90.0f / (Math.Abs(inputHorizontal) + Math.Abs(inputVertical)));

                alyuvarNowRotationValueZ = alyuvarNowRotationValueY * -1;
            }
            else
            {
                alyuvarNowRotationValueY = 0;
                alyuvarNowRotationValueZ = 0;
            }

        }

        Debug.LogWarning("alyuvarNowRotationValueX: " + alyuvarNowRotationValueX);
        Debug.LogWarning("alyuvarNowRotationValueY: " + alyuvarNowRotationValueY);
        Debug.LogWarning("alyuvarNowRotationValueZ: " + alyuvarNowRotationValueZ);
        */


    }

    private void rotateAlyuvarNormalRatation()
    {
        
        if (alyuvarNowRotationValueX < 0.025f && alyuvarNowRotationValueX > -0.025f)
        {
            alyuvarNowRotationValueX = 0;
        }

        if (alyuvarNowRotationValueX > 0)
        {
            alyuvarNowRotationValueX -= alyuvarRotationMaxiumValueX * Time.deltaTime * turnNormalRotationSpeed;
        }
        else if (alyuvarNowRotationValueX < 0)
        {
            alyuvarNowRotationValueX += alyuvarRotationMaxiumValueX * Time.deltaTime * turnNormalRotationSpeed;
        }
        
    }

    private void rotateAlyuvar() 
    {
        // Sa�a sola d�nme
        //transform.Rotate(Vector3.up, inputHorizontal * rotationSpeed * Time.deltaTime);

        // �leri geri d�nme (opsiyonel)
        //transform.Rotate(Vector3.right, inputVertical * rotationSpeed * Time.deltaTime);


        if ((nowRotationValueX <= startRotationValueX + alyuvarRotationMaxiumValueX 
            || (nowRotationValueX >= startRotationValueX + alyuvarRotationMaxiumValueX && inputHorizontal < 0)))
        {
            //Debug.Log("D�nd�r�l�yor 3");
            //transform.Rotate(Vector3.up, inputHorizontal * rotationSpeed * Time.deltaTime);
        }

        if (nowRotationValueX > startRotationValueX - alyuvarRotationMaxiumValueX
            || (nowRotationValueX <= startRotationValueX - alyuvarRotationMaxiumValueX && inputHorizontal > 0))
        {
            Debug.Log("D�nd�r�l�yor 4");
            transform.Rotate(Vector3.up, inputHorizontal * rotationSpeed * Time.deltaTime);
        }


        if (nowRotationValueX <= startRotationValueX + alyuvarRotationMaxiumValueX || inputVertical > 0)
        {
            //Debug.Log("D�nd�r�l�yor");
            //transform.Rotate(rotationY, 0, 0);
        }

        if (nowRotationValueX >= startRotationValueX - alyuvarRotationMaxiumValueX || inputVertical > 0)
        {
            //Debug.Log("D�nd�r�l�yor 2");
            //transform.Rotate(rotationY, 0, 0);
        }

        /*
        if (nowRotationValueX <= startRotationValueX + alyuvarRotationMaxiumValueX && inputVertical > 0)
        {
            Debug.Log("D�nd�r�l�yor 1");
            transform.Rotate(0, rotationY, 0);
        }
        else if (nowRotationValueX >= startRotationValueX - alyuvarRotationMaxiumValueX && inputVertical < 0)
        {
            Debug.Log("D�nd�r�l�yor 2");
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
        Debug.Log("Temastan ��kt�");

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
