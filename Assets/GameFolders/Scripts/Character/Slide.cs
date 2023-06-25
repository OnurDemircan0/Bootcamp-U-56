using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slide : MonoBehaviour
{
    ThirdPersonShooterController shooterController;
    ThirdPersonController personController;

    [SerializeField] LayerMask slideLayer;

    public bool Sliding = true;

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
    }

    private void Update()
    {
        CharacterSlide();
    }
    void CharacterSlide()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - personController.GroundedOffset,
                transform.position.z);
        Sliding = Physics.CheckSphere(spherePosition, personController.GroundedRadius, slideLayer,
            QueryTriggerInteraction.Ignore);

        if(Sliding)
        {
            Debug.Log("Sliding");
            animator.SetLayerWeight(4, Mathf.Lerp(animator.GetLayerWeight(4), 1f, Time.deltaTime * 10f));

            characterTransform.rotation = Quaternion.Euler(0f, characterTransform.rotation.y - 90f, 0f);

            Vector3 fowardDirection = new Vector3(0f, 0f, -1f).normalized;

            characterController.Move(fowardDirection * speed * Time.deltaTime);

            shooterController.DisableScript();
        }
        else
        {
            shooterController.EnableScript();
            animator.SetLayerWeight(4, Mathf.Lerp(animator.GetLayerWeight(4), 0f, Time.deltaTime * 10f));
        }
    }
}

