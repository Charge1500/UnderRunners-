using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Objects
{
    public int attack=3;
    protected override void OnConsumed(GameObject player){
        Player getPlayer = player.GetComponent<Player>();
        getPlayer.currentAttack+= attack;
        Destroy(gameObject);
    }
}
