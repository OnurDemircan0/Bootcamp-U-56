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

        currentSelectedWeapon = MedicineSwitching.selectedWeapon;
    }

    void Update()
    {
        if(currentSelectedWeapon != MedicineSwitching.selectedWeapon)
        {
            currentSelectedWeapon = MedicineSwitching.selectedWeapon;
            _animator.SetTrigger("WeaponChanged");
            _animator.SetInteger("SelectedWeapon", currentSelectedWeapon);
        }
    }
}
