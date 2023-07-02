using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunFiring : MonoBehaviour
{
    [SerializeField] float fadeDuration;
    AudioSource audioSource;
    StarterAssetsInputs starterAssets;

    private float initialVolume;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        starterAssets = GetComponent<StarterAssetsInputs>();
        initialVolume = audioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying && starterAssets.shoot)
        {
            PlayClip();
        }
        else if(audioSource.isPlaying && !starterAssets.shoot)
        {
            StartCoroutine(FadeOutAudio());
        }
    }

    void PlayClip()
    {
        audioSource.Play();
    }

    private System.Collections.IEnumerator FadeOutAudio()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            audioSource.volume = Mathf.Lerp(initialVolume, 0.0f, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = initialVolume;
    }
}
