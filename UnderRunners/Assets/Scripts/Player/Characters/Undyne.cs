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
    this.habDuration=2.0f; 
    //Dialog
    this.dialogStartTurn="¡AQUÍ VAMOS! ¡Prepárate para un espectáculo inolvidable!";
    this.dialogAttack="¡Siente el poder de Undyne! ¡Esto va a ser una locura!";
    this.dialogDamage="¡¿En serio?! ¡Eso es todo lo que tienes?! ¡Vamos, dame más!";
    this.dialogHab="¡Observa esto! ¡Una lanza y luego, ¡BOOM! ¡El rayo de la victoria!";
    this.dialogHeal="¡Oh sí! ¡Estoy lista para más! ¡No hay quien me detenga ahora!";
    this.dialogFoundRuby="¡LO TENGO! ¡Soy invencible! ¡Nadie puede conmigo!";
    }

    public GameObject undyneSpear;
    public override void ActiveHab(){
        if(currentColdown==0){
            turnOf.isWaitingForAbilityClick=true;
        }
    }

    public override void ActivateSkillOnPath(Vector3 targetPosition)
    {
        turnOf.UpdateDialogText(dialogHab,3);
        this.UseHab();
        undyneSpear.transform.position = targetPosition;
        undyneSpear.SetActive(true);
    }
}
