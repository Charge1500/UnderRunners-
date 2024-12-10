using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : Traps
{
    public void IsPlayerInside(){
        if(isPlayerInside==true){
            Player player=playerCollider.GetComponent<Player>();
            player.TakeDamage(2);
        }

    }
}
