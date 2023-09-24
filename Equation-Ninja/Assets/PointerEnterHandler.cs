using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEnterHandler : MonoBehaviour
{
    public void OnPointerEnter()
    {

        Debug.Log("Mouse passou sobre o objeto de UI: " + gameObject.name);
        //Destroy(eventData.pointerEnter); // Destruir o objeto de UI após a seleção
    }
}
