using Cinemachine;
using UnityEngine;

public class ClampVirtualCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Vector3 minPosition;
    public Vector3 maxPosition;

    private void Update()
    {
        // Get the current position of the virtual camera
        Vector3 currentPosition = virtualCamera.transform.position;

        // Clamp the position within the defined range
        currentPosition.x = Mathf.Clamp(currentPosition.x, minPosition.x, maxPosition.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, minPosition.y, maxPosition.y);
        currentPosition.z = Mathf.Clamp(currentPosition.z, minPosition.z, maxPosition.z);

        // Set the clamped position to the virtual camera
        virtualCamera.transform.position = currentPosition;
    }
}
