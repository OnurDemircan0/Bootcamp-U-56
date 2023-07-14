using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleEffect : MonoBehaviour
{
    public int childIndex = 25;
    public Transform muzzle;
    GameObject[] muzzleEffect;
    [SerializeField] StarterAssetsInputs starterInputs;

    ThirdPersonShooterController shooterController;

    bool onshoot = false;
    Coroutine muzzleAnimationCoroutine;

    private void Start()
    {
        shooterController = GetComponent<ThirdPersonShooterController>();
        muzzleEffect = new GameObject[childIndex];
        GoThrough();
    }

    void GoThrough()
    {
        for (int i = 0; i < childIndex; i++)
        {
           // Debug.Log("ITERATION " + i);

            Transform childTransform = muzzle.GetChild(i);
            muzzleEffect[i] = childTransform.gameObject;
           // Debug.Log("Child Object " + i + ": " + muzzleEffect[i].name);
        }
    }
    private void Update()
    {
        onshoot = starterInputs.shoot;
        bool burnedOut = shooterController.burnedOut;

        if (onshoot && muzzleAnimationCoroutine == null && !burnedOut)
        {
            muzzleAnimationCoroutine = StartCoroutine(PlayMuzzleAnimation());
        }
        else if ((!onshoot  || burnedOut ) && muzzleAnimationCoroutine != null)
        {
            StopCoroutine(muzzleAnimationCoroutine);
            muzzleAnimationCoroutine = null;
            SetItAllFlase();
        }
    }
    IEnumerator PlayMuzzleAnimation()
    {
        int index = 0;
        while (onshoot)
        {
            if (index >= muzzleEffect.Length)
                index = 0;

           // Debug.Log("Activating Child Object " + index);
            muzzleEffect[index].SetActive(true);
            yield return new WaitForSeconds(0.04f);

           // Debug.Log("Deactivating Child Object " + index);
            muzzleEffect[index].SetActive(false);

            index++;
        }
    }

    void SetItAllFlase()
    {
        for (int i = 0; i < childIndex; i++)
        {
            muzzleEffect[i].SetActive(false);
        }
    }
}