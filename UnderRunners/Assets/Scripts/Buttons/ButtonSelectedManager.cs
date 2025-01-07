using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelectedManager : MonoBehaviour
{
    private GameObject lastSelectedButton; 

    void Update()
    {
        
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            // Restablece la selección al último botón seleccionado
            if (lastSelectedButton != null)
            {
                EventSystem.current.SetSelectedGameObject(lastSelectedButton);
            }
        }
        else
        {
            // Actualiza el último botón seleccionado
            lastSelectedButton = EventSystem.current.currentSelectedGameObject;
        }
    }
}
