using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PapyrusInfo : SelectionCharacterSelected
{
    public PapyrusInfo(){
    this.health=7;
    this.attack=5;
    this.speed=4;
    this.description="Papyrus invoca un formidable muro compuesto de huesos que bloquea completamente el paso de los jugadores. Esta barrera sólida y resistente sirve para controlar el campo de batalla, permitiendo a Papyrus posicionarse estratégicamente mientras los enemigos se ven obligados a buscar rutas alternativas.(No se activara si hay jugadores muy cerca)";
    } 
}
