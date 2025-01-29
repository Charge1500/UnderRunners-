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
        if(getPlayer.currentHealth*health<=15){
            getPlayer.currentHealth = Mathf.CeilToInt(getPlayer.currentHealth * health);
        }
        getPlayer.transform.localScale *= scale;
        if(getPlayer.transform.localScale.y>2){
            getPlayer.transform.localScale= new Vector3(2,2,0);
        }

        if(getPlayer.transform.localScale.y<0.5f){
            getPlayer.transform.localScale= new Vector3(0.5f,0.5f,0);
        }

        if(getPlayer.currentAttack*attack<=12){
            getPlayer.currentAttack = Mathf.CeilToInt(getPlayer.currentAttack * attack);
        }

        if(getPlayer.currentSpeed / speed<=7){
            getPlayer.currentSpeed = Mathf.CeilToInt(getPlayer.currentSpeed / speed);
        }
    }
}
