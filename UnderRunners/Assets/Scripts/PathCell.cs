using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCell : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D someone)
    {
        if (someone.CompareTag("Player"))
        {
            TurnOf turnOf = someone.GetComponentInParent<TurnOf>();
            Player player = turnOf.turns[turnOf.currentTurnIndex];

            // Verifica si es el turno del jugador que entró
            if (player == someone.GetComponent<Player>() && player.isTurn)
            {
                
                turnOf.PlayerEnteredNewCell(player);
            }
        }
    }
}
