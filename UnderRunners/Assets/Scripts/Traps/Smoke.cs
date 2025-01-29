using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : Traps
{
    private MazeGenerator mazeGenerator; 

    void Start()
    {
        position=transform.position;
        mazeGenerator = GetComponentInParent<MazeGenerator>();
        soundEffectManager = GetComponentInParent<SoundEffectManager>();
        audioSource=soundEffectManager.audioSource;
    }
   public void IsPlayerInside(){
        if(isPlayerInside==true){
            Player player=playerCollider.GetComponent<Player>();
            List<(int, int)> paths = mazeGenerator.Paths();
            int x=0;
            int y=0;
            (x,y) = paths[Random.Range(0,paths.Count)];
            player.transform.position = new Vector3(x,y,0);
        }

    }
}
