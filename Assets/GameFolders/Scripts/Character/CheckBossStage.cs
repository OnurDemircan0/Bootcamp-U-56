using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossStage : MonoBehaviour
{
    [SerializeField]
    Death death;

    [SerializeField]
    GameObject mimic;

    [SerializeField]
    CinemachineVirtualCamera bossCam;

    [SerializeField]
    float bossCamSpeed = 1f;

    [SerializeField]
    float returnToPlayerTime = 4f;

    [SerializeField]
    ThirdPersonController thirdPersonController;

    [SerializeField]
    StarterAssetsInputs assetsInputs;

    bool startBossPath = false;
    bool InvokedReturnToPlayer = false;
    bool onTriggeredEnterTriggeredOnce = false;
    bool invokeIsCalled = false;
    bool onCutScene;
    private void OnTriggerEnter(Collider other)
    {
        if (!onTriggeredEnterTriggeredOnce)
        {
            death.onMimicStage = true;

            mimic.SetActive(true);

            if (!startBossPath)
            {
               // Debug.Log("Here in Invoke");
                Invoke("StartBossCamPath", 1f);
            }

            onTriggeredEnterTriggeredOnce=true;
        }
    }

    private void Update()
    {
        if (startBossPath && !InvokedReturnToPlayer)
        {
            bossCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = Mathf.Lerp(bossCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition, 3f, Time.deltaTime * bossCamSpeed);
            if(!invokeIsCalled)
            {
                Invoke("ReturnToPlayer", returnToPlayerTime + 8);
                invokeIsCalled=true;
            }
            
        }

        if(onCutScene)
        {
            assetsInputs.shoot = false;
            assetsInputs.aim = false;
            assetsInputs.move = Vector2.zero;
            assetsInputs.look = Vector2.zero;

        }
    }


    void StartBossCamPath()
    {
        onCutScene = true;

        bossCam.gameObject.SetActive(true);
 
        Invoke("SetStartBossPath", 8f);
    }

    void ReturnToPlayer()
    {
        onCutScene=false;

        bossCam.gameObject.SetActive(false);
        InvokedReturnToPlayer = true;
    }

    void SetStartBossPath()
    { startBossPath = true; }
}
