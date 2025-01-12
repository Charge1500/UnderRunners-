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
    }

    public GameObject copyFrisk;
    public GameObject habEffect;

    public override void ActiveHab(){
        if(currentColdown==0){
            copyFrisk.SetActive(true);
            habEffect.SetActive(true);
            this.UseHab();
        }
    }
}
