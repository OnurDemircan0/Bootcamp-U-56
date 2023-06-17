using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchingAnimation : MonoBehaviour
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
        if(currentSelectedWeapon != WeaponSwitching.selectedWeapon)
        {
            Debug.Log("girdi");
            currentSelectedWeapon = WeaponSwitching.selectedWeapon;
            _animator.SetInteger("SelectedWeapon", currentSelectedWeapon);
            _animator.SetTrigger("WeaponChanged");
        }
    }
}
