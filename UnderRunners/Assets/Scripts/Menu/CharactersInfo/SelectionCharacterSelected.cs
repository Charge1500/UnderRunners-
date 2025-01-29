using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SelectionCharacterSelected : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Button button;
    private Animator animator;
    public Sprite characterImage;
    public int health;
    public int attack;
    public int speed;
    protected string description;
    public TextMeshProUGUI textHealth;
    public TextMeshProUGUI textAttack;
    public TextMeshProUGUI textSpeed;
    public TextMeshProUGUI textDescription;
    public Image visibleCharacterImage;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(button.interactable){
            animator.SetBool("Selected", true); // Activar animación
            textHealth.text = health.ToString();
            textAttack.text = attack.ToString();
            textSpeed.text = speed.ToString();
            textDescription.text = description;
            visibleCharacterImage.sprite=characterImage;
        }  
    }

    public void OnDeselect(BaseEventData eventData)
    {
        animator.SetBool("Selected", false); // Desactivar animación
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(button.interactable){
            animator.SetBool("Selected", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Selected", false);
    }
}
