using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    private float oldSpeed;
    void OnTriggerEnter2D(Collider2D someone)
    {
        if (someone.CompareTag("Player"))
        {
            TurnOf turnOf = someone.GetComponentInParent<TurnOf>();
            Player player = turnOf.turns[turnOf.currentTurnIndex];

            // Verifica si es el turno del jugador que entró
            if (player == someone.GetComponent<Player>() && player.isTurn)
            {   
                oldSpeed=player.currentSpeed;
                player.currentSpeed=Mathf.CeilToInt(player.currentSpeed/3);
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
            player.currentSpeed=oldSpeed;  
            }
        }
    }
}
