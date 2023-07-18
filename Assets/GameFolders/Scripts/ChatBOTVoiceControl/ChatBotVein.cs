using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBotVein : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            _animator.SetTrigger("Continue");
        }
    }
}
