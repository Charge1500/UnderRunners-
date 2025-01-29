using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Objects
{
    public int addHealth = 2;
    protected override void OnConsumed(GameObject player){
        turnOf.UpdateDialogText(turnOf.turns[turnOf.currentTurnIndex].dialogHeal,4);
        Player getPlayer = player.GetComponent<Player>();
        if(getPlayer.currentHealth+addHealth <=10){
        getPlayer.currentHealth+= addHealth;
        }
    }
}
