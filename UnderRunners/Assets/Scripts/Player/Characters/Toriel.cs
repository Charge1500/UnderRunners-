using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toriel : Player
{
    public Toriel(){
    this.playerName="Toriel";
    this.health=8;
    this.attack=3;
    this.speedMovement=2;
    this.coldown=2;
    this.habDuration=2.5f;
    //Dialog
    this.dialogStartTurn="Es mi turno. Haré todo lo posible para avanzar.";
    this.dialogAttack="Perdóname, pero esto es necesario.";
    this.dialogDamage="¡Oh, eso dolió... pero debo seguir adelante!";
    this.dialogHab="Un momento para mí... Un té cálido hará maravillas.";
    this.dialogHeal="Gracias, me siento mejor ahora.";
    this.dialogFoundRuby="He encontrado lo que necesitaba.";
    }

    public GameObject torielHealing;
    public override void ActiveHab(){
        if(currentColdown==0){
            turnOf.UpdateDialogText(dialogHab,3);
            torielHealing.SetActive(true);
            Color currentColor = this.sr.color;
            this.sr.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);
            this.currentHealth+=2;
            this.UseHab();
            this.sr.color = new Color(currentColor.r, currentColor.g, currentColor.b, 255f);
            turnOf.UpdateUI(); 
        }
    }
}
