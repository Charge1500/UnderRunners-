using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTrap : Traps
{
    private TurnOf turnOf;

    void Start(){
        turnOf = GetComponentInParent<TurnOf>();
    }

    public IEnumerator IsPlayerInside(){
        if(isPlayerInside==true){

            Player player=playerCollider.GetComponent<Player>();
            player.isTurn=false;
            player.transform.position = transform.position;

            player.TakeDamage(1);
            player.isUsingHab=true;
            yield return new WaitForSeconds(0.8f);
            player.isUsingHab=false;
            Destroy(gameObject);
            turnOf.NextTurn();

        } else{
            yield return new WaitForSeconds(0.8f);
            animator.SetTrigger("Off");
        }
    }
}
