using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteIntensity : MonoBehaviour
{
    [SerializeField] float startIntensity = 0f;
    [SerializeField] float pulseIntensity = 1f;
    [SerializeField] float returnSpeed = 5f;

    private Volume volume;
    private Vignette vignette;

    private float currentIntensity;

    private void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);

        currentIntensity = startIntensity;
    }

    private void Update()
    {
        if (currentIntensity != startIntensity)
        {
            currentIntensity = Mathf.Lerp(currentIntensity, startIntensity, Time.deltaTime * returnSpeed);
            UpdateVignetteIntensity();
        }
    }

    private void UpdateVignetteIntensity()
    {
        vignette.intensity.value = currentIntensity;
    }

    public void Pulse()
    {
        currentIntensity = pulseIntensity;
        UpdateVignetteIntensity();
    }

    public void PulseDebug()
    {
        Debug.Log("Pulse");
    }
}
