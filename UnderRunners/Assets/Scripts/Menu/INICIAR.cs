using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class INICIAR : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject text;
    private Animator animator;
    void Awake()
    {
        animator = text.GetComponent<Animator>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        animator.SetBool("Selected", true); // Activar animación
    }

    public void OnDeselect(BaseEventData eventData)
    {
        animator.SetBool("Selected", false); // Desactivar animación  
    }
}
