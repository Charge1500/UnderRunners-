using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriskHab : Ability
{
    public bool ghostMode=false;
    
    public FriskHab(){
    this.abilityName="Determinación";
    this.cooldown=3;
    this.description="Te vuelves temporalmente intangible, permitiéndote \"atravesar\" paredes y obstáculos del laberinto durante este turno";
    }
}
