using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchingUI : MonoBehaviour
{
    private Animator _animator;

    private int currentSelectedWeapon;
    
    void Start()
    {
        _animator = GetComponent<Animator>();

        currentSelectedWeapon = WeaponSwitching.selectedWeapon;
    }

    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            currentSelectedWeapon = WeaponSwitching.selectedWeapon;
            _animator.SetTrigger("WeaponChanged");
            _animator.SetInteger("SelectedWeapon", currentSelectedWeapon);
        }
    }
}
