using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomColorController : MonoBehaviour
{
    public int nowVirusMaterialColorsNumber = -1;

    private Material virusMaterial;

    public Color[] virusMaterialColors;


    void Awake()
    {
        virusMaterial = gameObject.GetComponent<Renderer>().material;

        if (nowVirusMaterialColorsNumber == -1)
        {
            nowVirusMaterialColorsNumber = Random.Range(0, virusMaterialColors.Length);
        }
        virusMaterial.color = virusMaterialColors[nowVirusMaterialColorsNumber];

    }

}
