using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public bool isPlayerInside=false;
    // Animator
    public Animator animator;
    public Collider2D playerCollider;

    void Awake(){
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D someone)
    {
        if (someone.CompareTag("Player"))
        {
            TurnOf turnOf = someone.GetComponentInParent<TurnOf>();
            Player player = turnOf.turns[turnOf.currentTurnIndex];

            // Verifica si es el turno del jugador que entró
            if (player == someone.GetComponent<Player>() && player.isTurn)
            { 
             animator.SetTrigger("SetActive"); 
             isPlayerInside=true;
             playerCollider=someone;  
            }
        }
    }
    void OnTriggerExit2D(Collider2D someone)
    {
        if (someone.CompareTag("Player"))
        {
            TurnOf turnOf = someone.GetComponentInParent<TurnOf>();
            Player player = turnOf.turns[turnOf.currentTurnIndex];

            // Verifica si es el turno del jugador que entró
            if (player == someone.GetComponent<Player>() && player.isTurn)
            { 
            isPlayerInside=false;  
            }
        }
    }
}
