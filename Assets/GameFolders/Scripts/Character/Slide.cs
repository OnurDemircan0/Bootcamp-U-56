using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slide : MonoBehaviour
{
    StarterAssetsInputs starterInputs;
    ThirdPersonShooterController shooterController;
    ThirdPersonController personController;

    [SerializeField] LayerMask slideLayer;

    public bool Sliding = true;
    bool slideTriggered = false;

    Animator animator;

    Transform characterTransform;

    CharacterController characterController;

    [SerializeField] float speed;

    private void Start()
    {
        shooterController = GetComponent<ThirdPersonShooterController>();
        personController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        characterTransform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
        starterInputs = GetComponent<StarterAssetsInputs>();
    }
    


    private void Update()
    {

        CharacterSlide();
    }
    public void CharacterSlide()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - personController.GroundedOffset,
                   transform.position.z);
        Sliding = Physics.CheckSphere(spherePosition, 0.45f, slideLayer,
            QueryTriggerInteraction.Ignore);

        if (Sliding)
        {
            Debug.Log("Sliding");

            StartCoroutine(PressW());
            animator.SetLayerWeight(4, Mathf.Lerp(animator.GetLayerWeight(4), 1f, Time.deltaTime * 10f));

            characterTransform.rotation = Quaternion.Euler(0f, characterTransform.rotation.y - 90f, 0f);

            Vector3 fowardDirection = new Vector3(0f, 0f, -1f).normalized;

            characterController.Move(fowardDirection * speed * Time.deltaTime);

            shooterController.DisableScript();

            slideTriggered = true;
        }
        else
        {
            if(slideTriggered)
            shooterController.EnableScript();
            animator.SetLayerWeight(4, Mathf.Lerp(animator.GetLayerWeight(4), 0f, Time.deltaTime * 10f));
            slideTriggered = false;
        }
    }

    IEnumerator PressW()
    {
        yield return new WaitForSeconds(0.3f);
        starterInputs.move.y = 1f;

        yield return new WaitForSeconds(1);

        starterInputs.move.y = 0f;

        yield break;
    }
}

