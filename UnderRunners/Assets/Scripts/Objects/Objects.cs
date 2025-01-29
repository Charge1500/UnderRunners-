using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objects : MonoBehaviour
{
    public TurnOf turnOf;
    public SoundEffectManager soundEffectManager;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public bool taked=false;
    void Start(){
        turnOf = GetComponentInParent<TurnOf>();
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
                if(!taked){
                    taked=true;
                    OnConsumed(someone.gameObject);
                    turnOf.UpdateUI();
                    audioSource.PlayOneShot(audioClips[0]);
                    Desactivate();
                }    
             
            }
        }
    }
    protected abstract void OnConsumed(GameObject player);

    public IEnumerator Wait(){
        yield return new WaitForSeconds(10f);
        transform.localScale=new Vector3(1,1,0);
        taked=false;
    }
    public void Desactivate()
    {   
        transform.localScale=new Vector3(0,0,0);
        StartCoroutine(Wait());
    }
}
