using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alphys : Player
{
    public Alphys(){
    this.isTurn=false;
    //Stats
    this.playerName="Alphys";
    this.health=7;
    this.attack=4;
    this.speedMovement=2;
    //Hab
    this.coldown=3;
    this.habDuration=1f;
    //Dialog
    this.dialogStartTurn="¡Oh, cielos, m-mi turno! ¡Voy a hacer todo lo posible!";
    this.dialogAttack="¡Aquí va ...! ¡Prepárate para mi súper ataque!";
    this.dialogDamage="¡Ah! ¡Eso dolió más de lo que esperaba...!";
    this.dialogHab="Eh, uh, e-espero que esto funcione... ¡Onda electromagnética! Oh, y-yeah...";
    this.dialogHeal="Uh, g-gracias... m-me siento un poco mejor ahora...";
    this.dialogFoundRuby="¡Oh, d-dios mío! ¡Lo tengo! ¡No puedo creerlo!";
    }

    public GameObject stunWave;

    public override void ActiveHab(){
        if(currentColdown==0){
            turnOf.UpdateDialogText(dialogHab,3);
            stunWave.SetActive(true);
            this.UseHab();
        }
    }
}