using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorielHab : Ability
{
    public int range=3;
    
    public TorielHab(){
    this.abilityName="Llamada Protectora";
    this.cooldown=2;
    this.description="Toriel invoca una línea de fuego que se extiende a través del laberinto : Los oponentes que crucen la línea de fuego recibirán daño";
    }
}
