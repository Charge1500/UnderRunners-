using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habs : MonoBehaviour
{
    private Player player;
    void Awake(){
        player = GetComponentInParent<Player>();
    }
    public void HabSound(){
        player.audioSource.PlayOneShot(player.audioClips[3]);
    }
}
