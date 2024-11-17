using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsgoreHab : Ability
{
    public int ratio=2;
    
    public AsgoreHab(){
    this.abilityName="Aura Intimidante";
    this.cooldown=2;
    this.description="En lugar de invertir los controles, Asgore genera un \"aura intimidante\" alrededor de si mismo que reduce la velocidad de los jugadores oponentes que se encuentren dentro de un radio determinado";
    }
}
