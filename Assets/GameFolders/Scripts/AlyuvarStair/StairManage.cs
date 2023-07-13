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

    [SerializeField]
    GameObject EndOfLevel;

    bool invokeTriggered = false;

    public void CallSetStairs()
    {
        StartCoroutine(SetStairs());
    }

    IEnumerator SetStairs()
    {
        yield return new WaitForSeconds(7f);

        for (int i = 0; i < alyuvar.Length; i++)
        {
            Vector3 currentPosition = alyuvar[i].transform.position;
            Vector3 newPosition = new Vector3(currentPosition.x - 3.85f, currentPosition.y, currentPosition.z);
            float startTime = Time.time;

            while (alyuvar[i].transform.position.x > newPosition.x)
            {
                float t = (Time.time - startTime) / stairTime;
                alyuvar[i].transform.position = Vector3.Lerp(currentPosition, newPosition, t);

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }

        if (!invokeTriggered)
        {
            Invoke("JumpBoostParticle", 6f);
            invokeTriggered = true;
        }
    }

    void JumpBoostParticle()
    {
        jumpBoostParticle.SetActive(true);

        jumpBoostSound.PlayOneShot(jumpBoostSound.clip);

        personController.JumpHeight *= 2f;

        EndOfLevel.SetActive(true);
    }
}
