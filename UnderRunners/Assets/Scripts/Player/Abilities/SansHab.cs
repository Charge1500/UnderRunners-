using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansHab : Ability
{
    public int ratio=4;
    
    public SansHab(){
    this.abilityName="Gaster Blaster";
    this.cooldown=2;
    this.description="El Gaster Blaster teletransporta a Sans a cualquier lugar en un radio de 4 casillas";
    }
}
