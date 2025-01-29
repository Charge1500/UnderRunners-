using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FriskInfo : SelectionCharacterSelected
{
    public FriskInfo(){
    this.health=6;
    this.attack=4;
    this.speed=4;
    this.description="Frisk crea una réplica exacta de sí mismo que engaña a los enemigos en el campo de batalla. Si algún jugador, incluyendo Frisk, ataca esta copia, el ataque se refleja automáticamente, haciendo que el atacante sufra el mismo daño. Esta habilidad no solo protege a Frisk, sino que también vuelve a sus enemigos vulnerables a sus propios ataques.(Tenga cuidado de activarlo y colocarlo encima de un muro)";
    }
}
