using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objects : MonoBehaviour
{
    public TurnOf turnOf;
    void Start(){
        turnOf = GetComponentInParent<TurnOf>();
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
             OnConsumed(someone.gameObject);
             turnOf.UpdateUI();  
            }
        }
    }
    protected abstract void OnConsumed(GameObject player);

}
