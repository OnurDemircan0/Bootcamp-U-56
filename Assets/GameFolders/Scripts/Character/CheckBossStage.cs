using Cinemachine;
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

    bool startBossPath = false;
    bool InvokedReturnToPlayer = false;
    bool onTriggeredEnterTriggeredOnce = false;
    bool invokeIsCalled = false;
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
    }


    void StartBossCamPath()
    {

        bossCam.gameObject.SetActive(true);
 
        Invoke("SetStartBossPath", 8f);
    }

    void ReturnToPlayer()
    {
        bossCam.gameObject.SetActive(false);
        InvokedReturnToPlayer = true;
    }

    void SetStartBossPath()
    { startBossPath = true; }
}
