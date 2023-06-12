using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBySword : MonoBehaviour
{
    bool canBeHurt;

    // Start is called before the first frame update
    void Start()
    {
        canBeHurt = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //Player yerine Sowrd tag'i eklenecek
        {
            if (!canBeHurt) return;
            StartCoroutine(HurtDelay());

            //decrease the health of the germ.
        }
    }

    IEnumerator HurtDelay()
    {
        canBeHurt = false;
        yield return new WaitForSeconds(1f);
        canBeHurt = true;
    }
}
