using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asgore : Player
{
    public Asgore(){
    this.playerName="Asgore";
    this.health=10;
    this.attack=9;
    this.speedMovement=1;
    this.isTurn=false; 
    this.coldown=3;
    this.habDuration=3f;
    //Dialog
    this.dialogStartTurn="Es mi turno, intentaré dar lo mejor de mí.";
    this.dialogAttack="Perdónenme, amigos, pero tengo que dar lo mejor de mí en esta competencia.";
    this.dialogDamage="¡Argh! Pero no puedo rendirme tan fácilmente...";
    this.dialogHab="¡Allá vamos! Siento el poder fluir por mis venas";
    this.dialogHeal="Gracias, me siento mejor ahora.";
    this.dialogFoundRuby="Al fin lo encontré... espero poder usarlo bien.";
    }
    public GameObject increaseSpeed;

    public override void ActiveHab(){
        if(currentColdown==0){
            turnOf.UpdateDialogText(dialogHab,3);
            increaseSpeed.SetActive(true);
            this.currentSpeed+=3;
            turnOf.UpdateUI();
            this.UseHab();

        }
    }
    
}