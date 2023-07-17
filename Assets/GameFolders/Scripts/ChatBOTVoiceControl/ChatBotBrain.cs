using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBotBrain : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] RuntimeAnimatorController[] animatorControllers;
    public static bool pass;
    public static bool canChangeAnimator;
    private bool covidEncountered = false;

    private void Awake()
    {
        pass = false;
        canChangeAnimator = false;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        
        switch (CheckPointSystem.checkPointNumber)
        {
            case 0:
                _animator.runtimeAnimatorController = animatorControllers[0];
                break;
            case 4:
                _animator.runtimeAnimatorController = animatorControllers[1];
                break;
        }
        
    }

    private void Update()
    {
        if(CheckPointSystem.checkPointNumber == 0)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("ChatBotBrain"))
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    _animator.SetInteger("AnimationNumber", 1);
                }
            }
            /*
            else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("ChatBotBrainContinue"))
            {
                if (pass)
                {
                    _animator.SetInteger("AnimationNumber", 2);
                }
            }
            else if (_animator.GetCurrentAnimatorStateInfo(0).IsName("ChatBotBrainMedicine"))
            {
                if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetAxis("Mouse ScrollWheel") != 0))
                {
                    _animator.SetInteger("AnimationNumber", 3);
                }
            }*/
        }
        else if(CheckPointSystem.checkPointNumber == 4)
        {
            if (!covidEncountered)
            {
                covidEncountered = true;
                _animator.runtimeAnimatorController = animatorControllers[1];
            }
        }
        

        /*
        switch (chatBotStage)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Tab) && pass)
                {
                    pass = false;
                    _animator.SetTrigger("Continue");
                }
                break;
            case 1:
                if((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetAxis("Mouse ScrollWheel") != 0) && pass)
                {
                    pass = false;
                    _animator.SetTrigger("Continue");
                }
                break;
        }*/
        /*
        if (canChangeAnimator)
        {
            canChangeAnimator = false;
            _animator.runtimeAnimatorController = animatorControllers[chatBotStage];
        }
        */
    }
}
