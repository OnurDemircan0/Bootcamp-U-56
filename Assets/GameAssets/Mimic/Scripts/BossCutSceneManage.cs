using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCutSceneManage : MonoBehaviour
{
    public bool onCutSceneBoss;

    [SerializeField]
    StarterAssetsInputs assetsInputs;
    private void Update()
    {
        if(onCutSceneBoss)
        {
            assetsInputs.move = Vector2.zero;
            assetsInputs.look = Vector2.zero;

            assetsInputs.aim = false;
            assetsInputs.shoot = false;
        }
    }
}
