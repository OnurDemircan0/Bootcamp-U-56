using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Vector3 lastCheckPointPosition;
    public bool checkPointTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkPointTriggered = true;
            lastCheckPointPosition = transform.position;
        }
    }
}
