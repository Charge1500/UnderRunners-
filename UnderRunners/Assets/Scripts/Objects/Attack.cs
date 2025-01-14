using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Objects
{
    public int attack=2;
    protected override void OnConsumed(GameObject player){
        Player getPlayer = player.GetComponent<Player>();
        if(getPlayer.currentAttack+attack <=12 && getPlayer.currentAttack+attack >=1){
            getPlayer.currentAttack+= attack;
           
        }
        Destroy(gameObject);
    }
}
