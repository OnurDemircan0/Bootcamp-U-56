using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBOTcheker : MonoBehaviour
{
    [SerializeField]
    AudioSource kanBirikintisiUyarı;

    [SerializeField]
    AudioSource gunkCheck;

    [SerializeField]
    AudioSource asitliKanBirikintisi;

    [SerializeField]
    GetEnemies getEnemies;

    [SerializeField]
    CheckBossStage CheckBossStage;

    bool triggeredOnceKanBirikintisi;
    bool triggeredOnceGunkCheck;
    bool triggeredOnceCheckBossStage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !getEnemies.enteryEnemiesAreDeath && !triggeredOnceKanBirikintisi && kanBirikintisiUyarı != null)
        {
            kanBirikintisiUyarı.Play();
            triggeredOnceKanBirikintisi = true;
        }

        if(other.CompareTag("VFXother") && getEnemies.phase1EnemiesAreDeath && !triggeredOnceGunkCheck && gunkCheck !=null)
        {
            gunkCheck.Play();
            triggeredOnceGunkCheck = true;
        }

        if(other.CompareTag("VFXother") && !triggeredOnceGunkCheck && gunkCheck != null)
        {
            gunkCheck.Play();
            triggeredOnceGunkCheck = true;
        }
    }

    private void Update()
    {
        if(CheckBossStage.cutSceneIsOver && !triggeredOnceCheckBossStage && asitliKanBirikintisi != null && CheckBossStage != null)
        {
            Invoke("CallAcidBloodChatBOT", 5f);
            triggeredOnceCheckBossStage = true;
        }
    }

    void CallAcidBloodChatBOT()
    {
        asitliKanBirikintisi.Play();
    }
}
