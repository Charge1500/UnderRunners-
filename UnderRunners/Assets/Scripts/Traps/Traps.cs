using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public bool isPlayerInside=false;
    // Animator
    public Animator animator;
    public Collider2D playerCollider;
    public Vector3 position;

    public SoundEffectManager soundEffectManager;
    public AudioSource audioSource;
    public AudioClip[] clips;
    void Awake(){
        animator = GetComponent<Animator>();
    }

    void Start(){
        position=transform.position;
        soundEffectManager = GetComponentInParent<SoundEffectManager>();
        audioSource=soundEffectManager.audioSource;
    }

    void OnTriggerEnter2D(Collider2D someone)
    {
        if (someone.CompareTag("Player"))
        {
            TurnOf turnOf = someone.GetComponentInParent<TurnOf>();
            Player player = turnOf.turns[turnOf.currentTurnIndex];

            // Verifica si es el turno del jugador que entró
            if (player == someone.GetComponent<Player>() && player.isTurn)
            { 
             animator.SetTrigger("SetActive"); 
             isPlayerInside=true;
             playerCollider=someone;  
            }
            
        }
    }
    void OnTriggerExit2D(Collider2D someone)
    {
        if (someone.CompareTag("Player"))
        {
            TurnOf turnOf = someone.GetComponentInParent<TurnOf>();
            Player player = turnOf.turns[turnOf.currentTurnIndex];

            // Verifica si es el turno del jugador que entró
            if (player == someone.GetComponent<Player>() && player.isTurn)
            { 
            isPlayerInside=false;  
            }
        }
    }

    public IEnumerator Wait(){
        yield return new WaitForSeconds(3f);
        transform.localScale=new Vector3(1,1,0);
        transform.position=position;
    }
    public void Desactivate()
    {
        transform.position=new Vector3(-100,0,0);
        transform.localScale=new Vector3(0,0,0);
        StartCoroutine(Wait());
    }

    public void AudioClip1(){
        audioSource.PlayOneShot(clips[0]);
    }
}
