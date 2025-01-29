using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frisk : Player
{
    public Frisk(){
    this.playerName="Frisk";
    this.health=6;
    this.attack=4;
    this.speedMovement=4; 
    this.coldown=4;
    this.habDuration=5f;
    //Dialog
    this.dialogStartTurn="Frisk, ¡no te atrevas a fallar esta vez! ¡Demuéstrales quién manda!";
    this.dialogAttack="¡Vamos, Frisk! ¡Acaba con ellos de una vez por todas!";
    this.dialogDamage="¡¿En serio, Frisk?! ¡¿Eso es todo lo que tienes?! ¡Mejor espabila!";
    this.dialogHab="¡Frisk, pon atención! Esta copia no se mueve, pero cualquiera que la toque... ¡lo va a lamentar!";
    this.dialogHeal="¿Crees que eso te salvará? ¡Qué adorable!";
    this.dialogFoundRuby="¡Por fin, Frisk! Ahora agárralo con fuerza y no dejes que nadie te lo quite.";
    }

    public GameObject copyFrisk;
    public GameObject habEffect;

    public override void ActiveHab(){
        if(currentColdown==0){
            turnOf.UpdateDialogText(dialogHab,3);
            copyFrisk.SetActive(true);
            habEffect.SetActive(true);
            this.UseHab();
        }
    }
}
