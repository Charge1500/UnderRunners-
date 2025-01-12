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
    }
    public GameObject increaseSpeed;

    public override void ActiveHab(){
        if(currentColdown==0){
            increaseSpeed.SetActive(true);
            this.currentSpeed*=3;
            this.UseHab();
        }
    }
    
}