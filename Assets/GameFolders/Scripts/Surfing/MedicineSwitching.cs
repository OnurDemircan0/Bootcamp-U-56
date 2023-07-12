using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicineSwitching : MonoBehaviour
{
    public static int selectedWeapon;
    public int currentSelectedWeapon;
    private int previousSelectedWeapon;

    [SerializeField] private Color[] virusMaterialColors;


    [SerializeField] Image medicineColorImage;
    [SerializeField] RawImage crossHairImage;


    void Start()
    {
        selectedWeapon = 0;
        SelectWeapon();

    }


    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            selectedWeapon--;
        }

        if(selectedWeapon < 0)
        {
            selectedWeapon = virusMaterialColors.Length - 1;
        }
        else if (selectedWeapon > virusMaterialColors.Length - 1)
        {
            selectedWeapon = 0;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedWeapon = previousSelectedWeapon;
        }

        if (currentSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    public void SelectWeapon()
    {
        previousSelectedWeapon = currentSelectedWeapon;
        currentSelectedWeapon = selectedWeapon;

        Debug.Log("virusMaterialColors[currentSelectedWeapon]: " + virusMaterialColors[currentSelectedWeapon]);

        medicineColorImage.color = new Color(virusMaterialColors[currentSelectedWeapon].r, virusMaterialColors[currentSelectedWeapon].g, virusMaterialColors[currentSelectedWeapon].b);
        crossHairImage.color = new Color(virusMaterialColors[currentSelectedWeapon].r, virusMaterialColors[currentSelectedWeapon].g, virusMaterialColors[currentSelectedWeapon].b);

    }
}
