using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sans : Player
{
    public Sans(){
    this.playerName="Sans";
    this.health=3;
    this.attack=9;
    this.speedMovement=1; 
    this.coldown=4;
    this.habDuration=2.0f; 
    }
    
    public GameObject sansPortal1;
    public GameObject sansPortal2;
    public override void ActiveHab(){
        if(currentColdown==0){
            turnOf.isWaitingForAbilityClick=true;
        }
    }

    public override void ActivateSkillOnPath(Vector3 targetPosition)
    {
        sansPortal1.SetActive(true);
        sansPortal2.transform.position = targetPosition;
        sansPortal2.SetActive(true);
        this.UseHab();
    }

    public override void UseHab(){
        currentColdown=coldown;
        turnOf.coldown.text = currentColdown.ToString();
        turnOf.habButton.interactable=false;
        StartCoroutine(UseHabEffect());
    }
    
}
