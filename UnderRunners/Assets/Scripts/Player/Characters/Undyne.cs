using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undyne : Player
{
    public Undyne(){
    this.playerName="Undyne";
    this.health=4;
    this.attack=6;
    this.speedMovement=3;
    this.coldown=4;
    this.habDuration=1.0f; 
    }

    public GameObject undyneSpear;
    public override void ActiveHab(){
        if(currentColdown==0){
            turnOf.isWaitingForAbilityClick=true;
        }
    }

    public override void ActivateSkillOnPath(Vector3 targetPosition)
    {
        this.UseHab();
        undyneSpear.transform.position = targetPosition;
        undyneSpear.SetActive(true);
    }
}
