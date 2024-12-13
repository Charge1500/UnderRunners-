using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : Objects
{
    public float health=1.5f;
    public float scale=2f;
    public float attack=1.5f;
    public float speed=3f;

    protected override void OnConsumed(GameObject player)
    {
        Player getPlayer = player.GetComponent<Player>();
        
        getPlayer.currentHealth = Mathf.FloorToInt(getPlayer.currentHealth * health);

        // Aumentando escala
        getPlayer.transform.localScale *= scale;

        // Aumentando ataque jugador
        getPlayer.currentAttack = Mathf.FloorToInt(getPlayer.currentAttack * attack);

        // Reducir la velocidad del jugador a un tercio
        getPlayer.currentSpeed /= speed;

        Destroy(gameObject);
    }
}
