using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    bool death;

    [SerializeField] GameObject deathCam;
    [SerializeField] Animator animator;
    [SerializeField] DisableScripts disableScripts;

    //ThirdPersonController personController;
    //[SerializeField] LayerMask BloodBottomLayer;

    private void OnTriggerEnter(Collider other)
    {
        deathCam.SetActive(true);
        StartCoroutine(DeathAnimation());
        disableScripts.DisableAllScripts();
    }


    IEnumerator DeathAnimation() 
    {
        float timer = 0f;
        float duration = 1f;
        float startWeight = animator.GetLayerWeight(5);
        float targetWeight = 1.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            float currentWeight = Mathf.Lerp(startWeight, targetWeight, t);
            animator.SetLayerWeight(5, currentWeight);
            yield return null;
        }

        animator.SetLayerWeight(5, targetWeight);
        yield break;
    }

   //void Die()
   //{
   //    Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - personController.GroundedOffset,
   //            transform.position.z);
   //    death = Physics.CheckSphere(spherePosition, personController.GroundedRadius, BloodBottomLayer,
   //        QueryTriggerInteraction.Ignore);
   //
   //    if(death)
   //    {
   //        animator.SetLayerWeight(5, Mathf.Lerp(animator.GetLayerWeight(5), 1f, Time.deltaTime * 10f));
   //        deathCam.SetActive(true);
   //    }
   //}
}
