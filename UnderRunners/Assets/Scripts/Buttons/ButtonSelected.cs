using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonSelected : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Animator textAnimator;
    private TextMeshProUGUI textComponent;

    void Awake()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        textAnimator = textComponent.GetComponent<Animator>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        textAnimator.SetBool("Hover", true); // Activar animación
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        textAnimator.SetBool("Hover", false); // Desactivar animación
        
    }
}
