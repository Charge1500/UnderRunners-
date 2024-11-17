using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapyrusHab : Ability
{
    public int duration=2;
    
    public PapyrusHab(){
    this.abilityName="Muro de huesos";
    this.cooldown=3;
    this.description="Papyrus usa sus huesos para construir un muro temporal que bloquea un pasillo del laberinto";
    }
}
