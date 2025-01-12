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
    }

    public GameObject torielHealing;
    public override void ActiveHab(){
        if(currentColdown==0){
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
