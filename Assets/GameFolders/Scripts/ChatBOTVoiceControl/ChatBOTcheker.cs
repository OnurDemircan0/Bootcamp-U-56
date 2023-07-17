using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBOTcheker : MonoBehaviour
{
    [SerializeField]
    SlideV2 slidev2;

    [SerializeField]
    AudioSource kalpGiriş;

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

    [SerializeField]
    CheckBossVein checkBossVein;

    bool triggeredOnceKanBirikintisi;
    bool triggeredOnceGunkCheck;
    bool triggeredOnceCheckBossStage;
    bool triggeredOnceKalpGiriş;
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

        if(other.CompareTag("VFXother") && !triggeredOnceGunkCheck && gunkCheck != null && checkBossVein.enteredBossVein)
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

        if(slidev2.isSliding && !triggeredOnceKalpGiriş && kalpGiriş != null && slidev2 != null)
        {
            Invoke("CallKalpGiriş", 0.45f);
            triggeredOnceKalpGiriş = true;
        }
    }

    void CallAcidBloodChatBOT()
    {
        asitliKanBirikintisi.Play();
    }

    void CallKalpGiriş()
    {
        kalpGiriş.Play();
    }
}
