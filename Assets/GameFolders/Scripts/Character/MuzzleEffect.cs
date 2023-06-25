using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleEffect : MonoBehaviour
{
    bool set = false;
    public Transform muzzle;
    GameObject[] muzzleEffect;
    public int childIndex = 25;
    [SerializeField] StarterAssetsInputs starterInputs;

    private void Start()
    {
        muzzleEffect = new GameObject[childIndex];
        GoThrough();
    }

    private void Update()

    {
        if (starterInputs.shoot)
        {
            StartCoroutine(PlayMuzzleAnimation());
            set = true;
        }
        else
        {
            set = false;
        }
    }
    void GoThrough()
    {
        for (int i = 0; i < childIndex; i++)
        {
            Debug.Log("ITERATÝON" + i);

            Transform ChildTransform = muzzle.GetChild(i);

            muzzleEffect[i] = ChildTransform.gameObject;
        }
    }

    IEnumerator PlayMuzzleAnimation()
    {
        for (int i = 0; i < childIndex; i++)
        {
            if(set)
            {
                MuzzleAnimation(muzzleEffect[i]);
                yield return new WaitForSeconds(0.04f);
            }
            else
            {
                i = childIndex;
            }
        }
    }

    void MuzzleAnimation(GameObject frame)
    {
        frame.SetActive(true);
        StartCoroutine(DeactivateAfterDelay(frame, 0.04f));
    }

    IEnumerator DeactivateAfterDelay(GameObject frame, float delay)
    {
        yield return new WaitForSeconds(delay);
        frame.SetActive(false);
    }
}
