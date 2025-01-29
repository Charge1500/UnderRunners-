using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource; // Componente para reproducir el audio.

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Este objeto persiste entre escenas.
            
            audioSource = GetComponent<AudioSource>();
            
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeTrack(AudioClip newTrack)
    {
        audioSource.clip=newTrack;
        audioSource.Play();    
    }

    public void SetVolume(float volume) { 
        audioSource.volume = volume;
    }
}
