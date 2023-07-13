using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopShooting : MonoBehaviour
{
    public static StopShooting Instance;

    [SerializeField]
    StarterAssetsInputs assetsInputs;

    public
    bool turnOffShoot;


    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if(turnOffShoot)
        {
            assetsInputs.shoot = false;
        }
    }

}
