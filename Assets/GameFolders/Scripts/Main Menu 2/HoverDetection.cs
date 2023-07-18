using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering;

    Transform _Transform;


    private void Start()
    {
        _Transform = GetComponent<Transform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        _Transform.LeanScale(new Vector3(1.9f, 1.6f, 1.3f), 0.2f);
        Debug.Log("Cursor is hovering over the object");
        // Perform actions when the cursor is hovering over the object
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        
        _Transform.LeanScale(new Vector3(1.66f, 1.42f, 1f), 0.2f);
        Debug.Log("Cursor is not hovering over the object");
        // Perform actions when the cursor is not hovering over the object
    }
}

