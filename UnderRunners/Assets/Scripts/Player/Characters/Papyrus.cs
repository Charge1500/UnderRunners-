using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Papyrus : Player
{
    public Papyrus(){
    this.playerName="Papyrus";
    this.health=7;
    this.attack=5;
    this.speedMovement=4; 
    this.coldown=4;
    this.habDuration=1f;
    }

    public GameObject papyrusWall;
    public override void ActiveHab(){
        if(currentColdown==0){
            turnOf.isWaitingForAbilityClick=true;
        }
    }

    public override void ActivateSkillOnPath(Vector3 targetPosition)
    {
        this.UseHab();
        papyrusWall.transform.position = targetPosition;
        papyrusWall.SetActive(true);

        // Guardamos la posición global del muro antes de desparentarlo
        Vector3 muroPos = papyrusWall.transform.position;
        // Desparentamos el muro (ya no depende del jugador)
        papyrusWall.transform.SetParent(null);
        // Mantenemos la posición global del muro
        papyrusWall.transform.position = muroPos;
    }
}
