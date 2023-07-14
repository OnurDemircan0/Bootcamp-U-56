using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DissolveObject : MonoBehaviour
{
    public GatePulse[] gateCubes;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    public Material[] skinnedMaterials;

    public bool StartDissolving;

    public bool insideBossVein = false;

    public float dissolveRate = 0.0125f;

    public float refreshRate = 0.025f;

    [SerializeField] ParticleSystem particleDissolve;

    public bool colliderTriggered;

    bool triggeredParticleOnce;
    bool firstPhaseDone;

    bool secondOneIsTriggered = false;
    bool thirdOneIsTriggered = false;

    [SerializeField] float particleDelayTime = 0.15f;


    private void Start()
    {
        if (skinnedMeshRenderer != null)
        {
            skinnedMaterials = skinnedMeshRenderer.materials;
        }

        else if (skinnedMaterials.Length >= 0)
        {
            while (skinnedMaterials[0].GetFloat("_DissolveAmount") > 0)
            {
                for (int i = 0; i < skinnedMaterials.Length; i++)
                {
                    skinnedMaterials[i].SetFloat("_DissolveAmount", 0);
                }
            }
        }
    }

    private void OnTriggerEnter()
    {
        if(GetEnemies.Instance.phase2CanStart && !firstPhaseDone)
        {
            StartCoroutine(Dissolve());

            if(!triggeredParticleOnce)
            {
                Invoke("StartParticle", particleDelayTime);
                triggeredParticleOnce = true;
            }

            foreach(GatePulse gatePulse in gateCubes)
            {
                gatePulse.pulseStart = true;
            }

            firstPhaseDone = true;
        }
        else if(GetEnemies.Instance.phase2IsDone && !secondOneIsTriggered)
        {
            StartCoroutine(Dissolve());

            if (!triggeredParticleOnce)
            {
                Invoke("StartParticle", particleDelayTime);
                triggeredParticleOnce = true;
            }

            foreach (GatePulse gatePulse in gateCubes)
            {
                gatePulse.pulseStart = true;
            }
            secondOneIsTriggered = true;
        }

        else if(insideBossVein && !thirdOneIsTriggered)
        {
            StartCoroutine(Dissolve());

            if (!triggeredParticleOnce)
            {
                Invoke("StartParticle", particleDelayTime);
                triggeredParticleOnce = true;
            }

            foreach (GatePulse gatePulse in gateCubes)
            {
                gatePulse.pulseStart = true;
            }
            thirdOneIsTriggered = true;
        }
    }

    void StartParticle()
    {
        particleDissolve.Play();
    }

    public IEnumerator Dissolve()
    {
        if (skinnedMaterials.Length >= 0)
        {
            float counter = 0;

            while (skinnedMaterials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for(int i = 0; i < skinnedMaterials.Length; i++)
                {
                    skinnedMaterials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
