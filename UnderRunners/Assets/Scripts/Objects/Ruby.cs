using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruby : Objects
{
    protected override void OnConsumed(GameObject player){
        turnOf.UpdateDialogText(turnOf.turns[turnOf.currentTurnIndex].dialogFoundRuby,5);
        Player getPlayer = player.GetComponent<Player>();
        getPlayer.hasRuby=true;
        Destroy(gameObject);      
    }
}
