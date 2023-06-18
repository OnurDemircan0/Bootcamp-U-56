using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public static int selectedWeapon;
    private int currentSelectedWeapon;
    private int previousSelectedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        selectedWeapon = 0;
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && selectedWeapon != transform.childCount - 1)
        {
            selectedWeapon++;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f && selectedWeapon != 0)
        {
            selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 1)
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
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

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }

        previousSelectedWeapon = currentSelectedWeapon;
        currentSelectedWeapon = selectedWeapon;
    }

}
