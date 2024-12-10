using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgTrap : Traps
{
    public void IsPlayerInside(){
        if(isPlayerInside==true){
            Player player=playerCollider.GetComponent<Player>();
            player.TakeDamage(1);
        }

    }
}
