using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnAwake : MonoBehaviour
{
    private void Awake()
    {
        CinemachineCameraShaker.Instance.ShakeCameraAim(2f, 1.5f, true);
        CinemachineCameraShaker.Instance.ShakeCameraFollow(2f, 1.5f, true);
    }
}
