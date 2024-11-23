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

            turnOf.PlayerEnteredNewCell(turnOf.turns[turnOf.currentTurnIndex]);
        }
    }
}
