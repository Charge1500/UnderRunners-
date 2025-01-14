using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Speed : Objects
{
    public float speed=2;
    protected override void OnConsumed(GameObject player){
        Player getPlayer = player.GetComponent<Player>();
        if(getPlayer.currentSpeed*speed<=7){
            getPlayer.currentSpeed = Mathf.CeilToInt(getPlayer.currentSpeed * speed);
        }
        Destroy(gameObject);
    }
}
