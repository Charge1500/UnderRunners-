using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndyneHab : Ability
{
    public int push=2;
    
    public UndyneHab(){
    this.abilityName="Lanzamiento escamoso";
    this.cooldown=2;
    this.description="Usa una lanza para empujar a un jugador un número determinado de casillas en una dirección específica";
    }
}
