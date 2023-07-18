using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickButton : MonoBehaviour
{
    AudioSource ClickAudio;

    HoverDetection _hoverDetection;

    Transform _transform;

    bool ClickedBool = false;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _hoverDetection = GetComponent<HoverDetection>();
        ClickAudio = GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        _hoverDetection.enabled = false;

        if(ClickedBool == false)
        {
            StartCoroutine(Clicked());
            Debug.Log("Heree");
            ClickedBool = true;
            ClickAudio.Play();

        }

    }
  
    IEnumerator Clicked()
    {
        if(ClickedBool == false)
        {
            _transform.LeanScale(new Vector3(1.3f, 1.1f, 0.7f), 0.1f);

            yield return new WaitForSeconds(0.1f);

            _transform.LeanScale(new Vector3(1.66f, 1.42f, 1f), 0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
