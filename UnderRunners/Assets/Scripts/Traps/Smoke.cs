using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : Traps
{
   public void IsPlayerInside(){
        if(isPlayerInside==true){
            Player player=playerCollider.GetComponent<Player>();
        }

    }
}
