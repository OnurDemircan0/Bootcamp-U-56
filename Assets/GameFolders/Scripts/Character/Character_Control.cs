using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character_Control : MonoBehaviour
{
    CharacterController _characterControl;

    Vector3 MoveDirection;
    Vector3 velocity;

    public float speed;
    public float gravity = -12.6f;
    public float GroundCheckRadius;
    public float JumpHeight = 5f;

    public bool grounded;

    public Transform cam;
    public Transform GroundCheck;

    Animator _animator;
    Transform _transform;

    float smoothturnvelocity;
    float smoothturntime = 0.01f;

    public LayerMask GroundMask;

    void Start()
    {
        _characterControl = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        cursor();


    }

    private void OnDrawGizmosSelected()
    {
        if (GroundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
    }

    void Update()
    {
        if (grounded)
        {
            _animator.SetBool("Grounded", true);
        }
        else
        {
            _animator.SetBool("Grounded", false);
        }


        grounded = Physics.CheckSphere(GroundCheck.position, GroundCheckRadius, GroundMask);

        if (grounded && velocity.y < 0f)
        {
            velocity.y = -2f;

            _animator.SetBool("Jump", false);
        }

        velocity.y += gravity * Time.deltaTime;
        _characterControl.Move(velocity * Time.deltaTime);


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MoveDirection = new Vector3(horizontal, 0f, vertical).normalized;


        if (Mathf.Abs(horizontal) > 0.01 || Mathf.Abs(vertical) > 0.01)
        {
            _animator.SetFloat("Speed", 1f);

            float Targetangle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDamp(transform.eulerAngles.y, Targetangle, ref smoothturnvelocity, smoothturntime);
            transform.rotation = Quaternion.Euler(0f, Targetangle, 0f);

            Vector3 movedtoirection = Quaternion.Euler(0f, Targetangle, 0f) * Vector3.forward;

            _characterControl.Move(movedtoirection.normalized * speed * Time.deltaTime);

            if (Input.GetButton("Fire1"))
            {
                _characterControl.Move(movedtoirection.normalized * (speed * 2) * Time.deltaTime);

                _animator.SetBool("Running", true);

            }
            else
            {
                _animator.SetBool("Running", false);
            }

        }
        else
        {
            _animator.SetFloat("Speed", 0f);
        }


        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump();
        }
    }

    void cursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void jump()
    {
        velocity.y = Mathf.Sqrt(JumpHeight * -2 * gravity);

        _animator.SetBool("Jump", true);

    }
}
