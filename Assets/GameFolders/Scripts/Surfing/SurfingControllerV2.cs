using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using PathCreation;


public class SurfingControllerV2 : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public float dstTravelled;

    [SerializeField] private Image imageBlack;


    [SerializeField] private bool alyuvarVerticalAndHorizontalMovementControl = true;


    

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
    public float alyuvarRotationMaxiumValueZ = 15.0f;

    public float maxDistanceFromPathPoint = 1.0f;
    private float maxDistanceFromPathPointY;
    private float maxDistanceFromPathPointX;

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


        //dstTravelled = startPoint;

        //transform.position = pathCreator.path.GetPoint(0);

        nearestPointNumberForCharacter = 0;
        nearestPointForCharacter = pathCreator.path.localPoints[nearestPointNumberForCharacter];

        /*
        maxDistanceFromPathPointX = (float) Math.Sqrt(Math.Pow(maxDistanceFromPathPoint, 2) / 2);
        maxDistanceFromPathPointY = (float) Math.Sqrt(Math.Pow(maxDistanceFromPathPoint, 2) / 2);

        Debug.Log("maxDistanceFromPathPointX: " + maxDistanceFromPathPointX);
        Debug.Log("maxDistanceFromPathPointY: " + maxDistanceFromPathPointY);
        */

        maxDistanceFromPathPointX = maxDistanceFromPathPoint;
        maxDistanceFromPathPointY = maxDistanceFromPathPoint;


    }


    void Update()
    {
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");


        if (alyuvarVerticalAndHorizontalMovementControl)
        {
            translationX = inputVertical * speed * Time.deltaTime;
            translationZ = inputHorizontal * speed * Time.deltaTime;
        }
        else
        {
            translationX = 0;
            translationZ = 0;
        }
        

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



        if (gameObject.transform.childCount > 0)
        {
            //Debug.Log("gameObject.transform.GetChild(0).gameObject.transform.GetChild(0): " + gameObject.transform.GetChild(0).gameObject.transform.GetChild(0));

            

            try
            {
                gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition
                | RigidbodyConstraints.FreezeRotation;
            }
            catch(Exception e)
            {

            }





            followPath();

            //Invoke("followPath", 3);
        }


        //blockFarFromPath();



    }

    private void followPath()
    {
        if (dstTravelled > 780 && speed < maxSpeed)
        {
            speed += speedAcceleration * Time.deltaTime;
        }
        else if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }

        if (dstTravelled >= finishPoint)
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

        if (xDistanceFromPathPoint2 > maxDistanceFromPathPointX)
        {
            xDistanceFromPathPoint2 = maxDistanceFromPathPointX;

            rotateAlyuvarNormalRatation(false,true);
        }
        else if (xDistanceFromPathPoint2 < maxDistanceFromPathPointX * -1)
        {
            xDistanceFromPathPoint2 = maxDistanceFromPathPointX * -1;

            rotateAlyuvarNormalRatation(false, true);
        }
        else
        {
            determineRotateValueOfAlyuvar(true, false);
        }



        yDistanceFromPathPoint1 = inputVertical * maxDistanceFromPathPoint * Time.deltaTime * 10;

        

        if (yDistanceFromPathPoint1 != yDistanceFromPathPoint2)
        {
            yDistanceFromPathPoint2 += yDistanceFromPathPoint1;
        }

        if (yDistanceFromPathPoint2 > maxDistanceFromPathPointY)
        {
            yDistanceFromPathPoint2 = maxDistanceFromPathPointY;


            rotateAlyuvarNormalRatation(true, false);
        }
        else if (yDistanceFromPathPoint2 < maxDistanceFromPathPointY * -1)
        {
            yDistanceFromPathPoint2 = maxDistanceFromPathPointY * -1;


            rotateAlyuvarNormalRatation(true, false);
        }
        else
        {
            determineRotateValueOfAlyuvar(false, true);
        }

        //Debug.Log("xDistanceFromPathPoint1: " + xDistanceFromPathPoint1);
        Debug.Log("xDistanceFromPathPoint2: " + xDistanceFromPathPoint2);

        //Debug.Log("yDistanceFromPathPoint1: " + yDistanceFromPathPoint1);
        Debug.Log("yDistanceFromPathPoint2: " + yDistanceFromPathPoint2);


        /*
        maxDistanceFromPathPointX = (float) Math.Sqrt(Math.Pow(maxDistanceFromPathPoint, 2) - Math.Pow(xDistanceFromPathPoint2, 2));
        maxDistanceFromPathPointY = (float) Math.Sqrt(Math.Pow(maxDistanceFromPathPoint, 2) - Math.Pow(yDistanceFromPathPoint2, 2));


        Debug.Log("maxDistanceFromPathPointX: " + maxDistanceFromPathPointX);
        Debug.Log("maxDistanceFromPathPointY: " + maxDistanceFromPathPointY);
        */



        /*
        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + xDistanceFromPathPoint2
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + yDistanceFromPathPoint2
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z);
        */



        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z);

        /*
        transform.position += new Vector3(transform.right.x * xDistanceFromPathPoint2,
            transform.right.y * xDistanceFromPathPoint2,
            transform.right.z * xDistanceFromPathPoint2);
        */


        if (alyuvarVerticalAndHorizontalMovementControl)
        {
            transform.position += new Vector3(transform.right.x * xDistanceFromPathPoint2,
            0 * xDistanceFromPathPoint2,
            transform.right.z * xDistanceFromPathPoint2);



            transform.position += new Vector3(transform.up.x * yDistanceFromPathPoint2,
                transform.up.y * yDistanceFromPathPoint2,
                transform.up.z * yDistanceFromPathPoint2);
        }

        


        /*
        transform.position += new Vector3(transform.forward.x * yDistanceFromPathPoint2,
            transform.forward.y * yDistanceFromPathPoint2,
            transform.forward.z * yDistanceFromPathPoint2);
        */

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


        if (alyuvarVerticalAndHorizontalMovementControl)
        {
            transform.rotation = Quaternion.Euler(new Vector3(pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.x + alyuvarNowRotationValueX * -1
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.y
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.z + 64 + alyuvarNowRotationValueZ));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.x
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.y
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.z + 90));
        }

        


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

        if (point1Distance < point2Distance)
        {
            nearestPointForCharacter = point1;
        }
        else
        {
            nearestPointForCharacter = point2;
            nearestPointNumberForCharacter++;
        }


        //Debug.Log("En Yakýn Nokta: " + nearestPointForCharacter);

        if (transform.position.x > nearestPointForCharacter.x + maxDistanceFromPathPoint)
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

    private void determineRotateValueOfAlyuvar(bool determineRotateValueOfAlyuvarControlX, bool determineRotateValueOfAlyuvarControlZ)
    {
        /*
        alyuvarNowRotationValueX = inputVertical * alyuvarRotationMaxiumValueX;
        alyuvarNowRotationValueY = 0;
        alyuvarNowRotationValueZ = inputHorizontal * alyuvarRotationMaxiumValueZ;
        */

        if (determineRotateValueOfAlyuvarControlX && determineRotateValueOfAlyuvarControlZ)
        {
            //alyuvarNowRotationValueX = inputVertical * alyuvarRotationMaxiumValueX;
            alyuvarNowRotationValueY = 0;
            //alyuvarNowRotationValueZ = inputHorizontal * alyuvarRotationMaxiumValueZ;
        }
        else if (determineRotateValueOfAlyuvarControlX)
        {
            //alyuvarNowRotationValueX = inputVertical * alyuvarRotationMaxiumValueX;
            alyuvarNowRotationValueY = 0;
            alyuvarNowRotationValueZ = inputHorizontal * alyuvarRotationMaxiumValueZ;
        }
        else if (determineRotateValueOfAlyuvarControlZ)
        {
            alyuvarNowRotationValueX = inputVertical * alyuvarRotationMaxiumValueX;
            alyuvarNowRotationValueY = 0;
            //alyuvarNowRotationValueZ = inputHorizontal * alyuvarRotationMaxiumValueZ;
        }

    }

    private void rotateAlyuvarNormalRatation(bool rotateAlyuvarNormalRatationControlX, bool rotateAlyuvarNormalRatationControlZ)
    {
        /*
        // X
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



        // Z
        if (alyuvarNowRotationValueZ < 0.025f && alyuvarNowRotationValueZ > -0.025f)
        {
            alyuvarNowRotationValueZ = 0;
        }

        if (alyuvarNowRotationValueZ > 0)
        {
            alyuvarNowRotationValueZ -= alyuvarRotationMaxiumValueZ * Time.deltaTime * turnNormalRotationSpeed;
        }
        else if (alyuvarNowRotationValueZ < 0)
        {
            alyuvarNowRotationValueZ += alyuvarRotationMaxiumValueZ * Time.deltaTime * turnNormalRotationSpeed;
        }
        */

        // X
        if (rotateAlyuvarNormalRatationControlX)
        {
            Debug.Log("rotateAlyuvarNormalRatationControlX alyuvarNowRotationValueX : " + alyuvarNowRotationValueX);

            Debug.Log("rotateAlyuvarNormalRatationControlX");

            if (alyuvarNowRotationValueX < 1.5f && alyuvarNowRotationValueX > -1.5f)
            {
                alyuvarNowRotationValueX = 0;

                Debug.Log("rotateAlyuvarNormalRatationControlX 1");
            }

            if (alyuvarNowRotationValueX > 0)
            {
                alyuvarNowRotationValueX -= alyuvarRotationMaxiumValueX * Time.deltaTime * turnNormalRotationSpeed;

                Debug.Log("rotateAlyuvarNormalRatationControlX 2");
            }
            else if (alyuvarNowRotationValueX < 0)
            {
                alyuvarNowRotationValueX += alyuvarRotationMaxiumValueX * Time.deltaTime * turnNormalRotationSpeed;

                Debug.Log("rotateAlyuvarNormalRatationControlX 3");
            }
        }

        // Z
        if (rotateAlyuvarNormalRatationControlZ)
        {
            if (alyuvarNowRotationValueZ < 1.5f && alyuvarNowRotationValueZ > -1.5f)
            {
                alyuvarNowRotationValueZ = 0;
            }

            if (alyuvarNowRotationValueZ > 0)
            {
                alyuvarNowRotationValueZ -= alyuvarRotationMaxiumValueZ * Time.deltaTime * turnNormalRotationSpeed;
            }
            else if (alyuvarNowRotationValueZ < 0)
            {
                alyuvarNowRotationValueZ += alyuvarRotationMaxiumValueZ * Time.deltaTime * turnNormalRotationSpeed;
            }
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
            //transform.Rotate(Vector3.up, inputHorizontal * rotationSpeed * Time.deltaTime);
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
            //collision.gameObject.transform.parent = gameObject.transform;

            collision.gameObject.transform.parent = gameObject.transform.GetChild(0);
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
