using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StarterAssets;

public class BlockCharacterFiring : MonoBehaviour
{

    [SerializeField] private StarterAssetsInputs starterAssetsInputs;

    void Update()
    {
        starterAssetsInputs.shoot = false;
    }

    private void FixedUpdate()
    {
        starterAssetsInputs.shoot = false;
    }

    private void LateUpdate()
    {
        starterAssetsInputs.shoot = false;

    }
}
