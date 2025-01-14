using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrap : MonoBehaviour
{
    public Animator animatorTrap;
    private Animator animator;

    void Awake(){
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D someone)
    {
        animator.SetBool("Press", true);
        animatorTrap.SetTrigger("SetActive");
    }
    void OnTriggerExit2D(Collider2D someone)
    {
        animator.SetBool("Press", false);
    }
}
