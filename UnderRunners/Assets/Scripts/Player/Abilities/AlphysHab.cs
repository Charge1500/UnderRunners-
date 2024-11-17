using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphysHab : Ability
{
    public int ratio=1;
    
    public AlphysHab(){
    this.abilityName="Showtime";
    this.cooldown=3;
    this.description="Invoca a mettaton para crear un espectáculo de luces y sonidos que aturde temporalmente a los enemigos en un radio pequeño";
    }
}
