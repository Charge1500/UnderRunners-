using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color hoverColor = new Color(1f, 0.05f, 0.05f); // Color rojo (#FF0D0D)
    private Button button;
    private Color originalColor;

    private void Awake()
    {
        button = GetComponent<Button>();
        originalColor = button.targetGraphic.color; // Guardar el color original
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
        {
            button.targetGraphic.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.targetGraphic.color = originalColor;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (button.interactable)
        {
            button.targetGraphic.color = hoverColor;
        }
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        button.targetGraphic.color = originalColor;
        
    }
}
