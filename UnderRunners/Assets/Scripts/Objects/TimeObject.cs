using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : Objects
{
    public float additionalTurnTime = 5.0f; // Tiempo adicional en segundos

    protected override void OnConsumed(GameObject player)
    {
        TurnOf turnOf = GetComponentInParent<TurnOf>();; // Encontrar el gestor de turnos en la escena
        turnOf.IncreaseTurnTime(additionalTurnTime); // Aumentar el tiempo del turno
        Destroy(gameObject);
    }
}
