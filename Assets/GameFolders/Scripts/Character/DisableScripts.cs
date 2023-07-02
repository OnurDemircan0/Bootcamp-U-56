using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScripts : MonoBehaviour
{
    MonoBehaviour[] Scripts;

    private void Start()
    {
       Scripts = GetComponents<MonoBehaviour>();
    }



    public void DisableAllScripts()
    {
        foreach(MonoBehaviour script in Scripts)
        {
            if (script == this) { continue; }

            else
            {
                script.enabled = false; 
            }
        }
    }
}
