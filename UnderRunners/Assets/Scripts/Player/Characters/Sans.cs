using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sans : Player
{
    public Sans(){
    this.playerName="Sans";
    this.health=3;
    this.attack=9;
    this.speedMovement=3; 
    this.coldown=4;
    this.habDuration=7.0f; 
    //Dialog
    this.dialogStartTurn="Supongo que es mi turno. Esto será interesante.";
    this.dialogAttack="No me odies por esto, es solo trabajo.";
    this.dialogDamage="Bueno, eso fue desagradable. Mueveme con mas cuidado humano jejeje";
    this.dialogHab="Toc...toc.Ahora me ves... ahora no.";
    this.dialogHeal="Ah, eso está mejor. No me hagas acostumbrarme a esto.";
    this.dialogFoundRuby="Bueno, mira lo que encontré. Tal vez las cosas se están poniendo a mi favor";
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
        turnOf.UpdateDialogText(dialogHab,3);
        sansPortal1.SetActive(true);

        Vector3 portal2Pos = sansPortal2.transform.position;
        sansPortal2.transform.SetParent(null);
        sansPortal2.transform.position = portal2Pos;

        sansPortal2.transform.position = targetPosition;
        sansPortal2.SetActive(true);
        UseHab();
    }

    public override void UseHab(){
        currentColdown=coldown;
        turnOf.coldown.text = currentColdown.ToString();
        turnOf.habButton.interactable=false;
        StartCoroutine(UseHabEffect());
    }
    
}
