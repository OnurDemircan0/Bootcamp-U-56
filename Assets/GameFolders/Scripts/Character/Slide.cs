using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Slide : MonoBehaviour
{
    [SerializeField]
    LayerMask slideLayer;

    [SerializeField]
    float speed;

    [SerializeField]
    float horizontalSpeed = 10f;

    ThirdPersonShooterController shooterController;
    ThirdPersonController personController;
    CharacterController characterController;

    bool Sliding = false;
    bool slideTriggered = false;

    Animator animator;
    Transform characterTransform;
    PlayerInput playerInput;

    private void Start()
    {
        shooterController = GetComponent<ThirdPersonShooterController>();
        personController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        characterTransform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        playerInput.enabled = false;
        shooterController.DisableScript();
    }

    private void Update()
    {
        CharacterSlide();
    }

    public void CharacterSlide()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - personController.GroundedOffset,
                   transform.position.z);

        Sliding = Physics.CheckSphere(spherePosition, 0.45f, slideLayer, QueryTriggerInteraction.Ignore);

        if (Sliding)
        {
            animator.SetLayerWeight(4, Mathf.Lerp(animator.GetLayerWeight(4), 1f, Time.deltaTime * 10f));

            characterTransform.rotation = Quaternion.Euler(0f, characterTransform.rotation.y - 90f, 0f);
            
            Vector3 forwardDirection = new Vector3(0f, 0f, -1f).normalized;

            if (Input.GetAxis("Horizontal") != 0)
            {
                forwardDirection.x = (-1 * Input.GetAxis("Horizontal")) * horizontalSpeed;
            }


            Vector3 moveVector = forwardDirection * speed * Time.deltaTime;
            characterController.Move(moveVector);

            StartCoroutine(FinishSlide());
        }
    }

    IEnumerator FinishSlide()
    {
        yield return new WaitForSeconds(5.38f);

        shooterController.EnableScript();
        animator.SetLayerWeight(4, Mathf.Lerp(animator.GetLayerWeight(4), 0f, Time.deltaTime * 10f));
        slideTriggered = false;

        playerInput.enabled = true;
        this.enabled = false;
    }
}
