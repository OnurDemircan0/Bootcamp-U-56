using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRandom : MonoBehaviour
{

    [SerializeField] private float maxRotationAngle;

    private float randomRotationX;
    private float randomRotationY;
    private float randomRotationZ;

    private void Start()
    {
        randomRotationX = Random.Range(maxRotationAngle * -1, maxRotationAngle);
        randomRotationY = Random.Range(maxRotationAngle * -1, maxRotationAngle);
        randomRotationZ = Random.Range(maxRotationAngle * -1, maxRotationAngle);
    }

    void Update()
    {
        transform.Rotate(randomRotationX, randomRotationY, randomRotationZ, Space.Self);

    }
}
