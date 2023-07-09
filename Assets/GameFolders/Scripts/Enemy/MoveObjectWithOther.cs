using UnityEngine;

public class MoveObjectWithOther : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    private Vector3 initialOffset;

    private void Start()
    {
        initialOffset = targetObject.position - transform.position;
    }

    private void Update()
    {
        transform.position = targetObject.position - initialOffset;
    }
}
