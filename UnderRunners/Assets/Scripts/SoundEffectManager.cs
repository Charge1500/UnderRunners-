using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioSource audioSource;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
}
    
