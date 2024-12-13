using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Objects
{
    public int addHealth = 2;
    protected override void OnConsumed(GameObject player){
        Player getPlayer = player.GetComponent<Player>();
        getPlayer.currentHealth+= addHealth;
        Destroy(gameObject);
    }
}
