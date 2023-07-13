using MimicSpace;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairManage : MonoBehaviour
{
    [SerializeField]
    GameObject[] alyuvar;

    [SerializeField]
    Movement mimicMovement;

    [SerializeField]
    float stairTime = 2f;

    [SerializeField]
    GameObject jumpBoostParticle;

    [SerializeField]
    AudioSource jumpBoostSound;

    [SerializeField]
    ThirdPersonController personController;

    bool invokeTriggered = false;

    public void CallSetStairs()
    {
        StartCoroutine(SetStairs());
    }

    IEnumerator SetStairs()
    {
        yield return new WaitForSeconds(7f);

        for (int i = 0; i < alyuvar.Length; i++ )
        {
            Vector3 newPosition = new Vector3((alyuvar[i].transform.position.x - 2f), alyuvar[i].transform.position.y, alyuvar[i].transform.position.z);

            while (alyuvar[i].transform.position.x > newPosition.x)
            {
                alyuvar[i].transform.position = Vector3.Lerp(alyuvar[i].transform.position, newPosition, Time.deltaTime * stairTime);
                yield return null;
            }
            
            yield return new WaitForSeconds(0.5f);
        }

        if(!invokeTriggered)
        {
            Invoke("JumpBoosParticle", 6f);
            invokeTriggered = true;
        }
        

        yield break;
    }


    void JumpBoosParticle()
    {
        jumpBoostParticle.SetActive(true);

        jumpBoostSound.PlayOneShot(jumpBoostSound.clip);

        personController.JumpHeight *= 2f;
    }
}
