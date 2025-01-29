using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Images
    public Sprite imageWinner;
    public Sprite imageDialogDefault;
    public Sprite image;
    // Stats
    public string playerName;
    public int health;
    public int currentHealth;
    public int attack;
    public int currentAttack;
    public int coldown;
    public int currentColdown;
    private Color originalColor;
    //Dialog Texts
    public string dialogStartTurn;
    public string dialogAttack;
    public string dialogDamage;
    public string dialogHab;
    public string dialogHeal;
    public string dialogFoundRuby;
    //Dialog Images
    public Sprite[] dialogImages;
    //-----------------
    public bool hasRuby = false;
    public bool isTurn = false;
    public bool isUsingHab = false;
    public float habDuration=1.0f;
    public bool stun = false;
    public int puntuation=0;

    // Movement
    public float speedMovement = 5f;
    public float currentSpeed;
    private Vector2 _movement;
    private Rigidbody2D rb;
    public SpriteRenderer sr;

    // Animator
    public Animator animator;

    public TurnOf turnOf;
    private Pencil pencil;
    public Vector2 respawnPoint;
    //Attack
    public List<Player> playersToAttack = new List<Player>();
    public bool friskCopy=false;
    //AudioSource
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        originalColor = sr.color;
        currentHealth=health;
        currentAttack=attack;
        currentSpeed=speedMovement;
        currentColdown=0;
    }

    void Start(){
        turnOf = GetComponentInParent<TurnOf>();
        pencil = GetComponentInParent<Pencil>();
    }
    void Update()
    {
        // Solo permitir movimiento si es el turno del jugador
        if (isTurn && !isUsingHab)
        {
            // Movimiento
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // ANIMACIONES
            if(moveHorizontal != 0 || moveVertical != 0){
                animator.SetBool("IsWalking", true);
            } else {
                animator.SetBool("IsWalking", false);
            }

            // Determinar la dirección del movimiento
            if (moveHorizontal != 0) {
                animator.SetInteger("WalkDirection", 2); // Horizontal
                Vector3 playerScale = transform.localScale;
                playerScale.x = Mathf.Abs(playerScale.x) * (moveHorizontal < 0 ? 1 : -1);
                transform.localScale = playerScale;
            } else if (moveVertical > 0) {
                animator.SetInteger("WalkDirection", 3); // Arriba
            } else if (moveVertical < 0) {
                animator.SetInteger("WalkDirection", 1); // Abajo
            }

            // Calcular el movimiento
            _movement = new Vector2(moveHorizontal, moveVertical);

            // Normalizar el vector de movimiento si su magnitud es mayor que 1
            if (_movement.magnitude > 1)
            {
                _movement.Normalize();
            }

            _movement *= currentSpeed * Time.deltaTime*0.3f;
        }
        else
        {
            _movement = Vector2.zero;
            animator.SetBool("IsWalking", false); // Detener la animación de caminar
        }
        if(currentHealth<0){
            currentHealth = 0;
            turnOf.UpdateUI();
        }
    }

    void FixedUpdate() {
        if (isTurn) {
            rb.MovePosition(rb.position + _movement);
        }
        sr.sortingOrder = (int)rb.position.y * -1;
    }

    public virtual void TakeDamage(int damage)
    {
        if(!isUsingHab){
            if(isTurn){
                turnOf.UpdateDialogText(dialogDamage,2);
            }
            currentHealth -= damage;
            if(currentHealth < 0) currentHealth=0;
                turnOf.UpdateUI();
            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
            } else{
                StartCoroutine(DamageEffect());
            }
        }
    }

    private IEnumerator DamageEffect()
    {
        audioSource.PlayOneShot(audioClips[0]);
        sr.color = Color.red;
        bool wasTurn = isTurn;
        isTurn=false;
        isUsingHab=true;
        yield return new WaitForSeconds(0.5f);
        if(wasTurn){
            isTurn=true;
        }
        isUsingHab=false;
        sr.color = originalColor;
    }

    private IEnumerator Die()
    {
        audioSource.PlayOneShot(audioClips[1]);
        if(puntuation>0){
            puntuation--;
        }
        sr.color = originalColor;
        stun=false;
        bool wasTurn=isTurn;
        isTurn=false;
        animator.SetBool("ExcludeDead",false);
        animator.SetTrigger("Dead"); 
        isUsingHab=(wasTurn) ? true : false;
        yield return new WaitForSeconds(0.6f);
        isUsingHab=(wasTurn) ? false : false;
        if(wasTurn){
            turnOf.NextTurn();
        }
        animator.SetBool("ExcludeDead",true);
        animator.SetBool("IsWalking",false);
        Respawn();
        if(hasRuby){
            hasRuby=false;
            pencil.InstantiateRuby(pencil.rubyRespawnPoint);
        }  
    }

    public void Respawn()
    { 
        RestoreOriginalStats();
        transform.position = respawnPoint;
        currentHealth = health;
        turnOf.UpdateUI();
    }

    public void RestoreOriginalStats() { 
        currentAttack = attack;
        currentSpeed = speedMovement; 
        transform.localScale= new Vector3(1,1,1);
    }

    public virtual void ActiveHab()
    {
        Debug.Log("Habilidad no definida para este jugador.");
    }
    public virtual void ActivateSkillOnPath(Vector3 targetPosition)
    {
        Debug.Log("Habilidad no definida para este jugador.");
    }

    public virtual void UseHab(){
        currentColdown=coldown;
        turnOf.coldown.text = currentColdown.ToString();
        turnOf.habButton.interactable=false;
        StartCoroutine(UseHabEffect());
    }

    public IEnumerator UseHabEffect()
    {
        turnOf.attackButton.interactable=false;
        isUsingHab = true;
        animator.SetBool("IsWalking", false);
        animator.SetInteger("WalkDirection", 1); // Abajo
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // Congela todo
        yield return new WaitForSeconds(habDuration); // Tiempo que tarda la habilidad en ejecutarse
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isUsingHab = false; // Reactiva el movimiento
    }

    private void OnTriggerEnter2D(Collider2D someone){
        if (someone.CompareTag("Player") && !isUsingHab && turnOf.oneAttack){ 
            Player player = someone.GetComponent<Player>();
            if (!playersToAttack.Contains(player) && player.playerName!=playerName){}
            {
                playersToAttack.Add(player);
            }
        }
        if (someone.CompareTag("FriskCopy") && !isUsingHab && turnOf.oneAttack){  
            friskCopy=true;
        }
        turnOf.attackButton.interactable=(playersToAttack.Count > 0||friskCopy) && turnOf.oneAttack;
    }

    private void OnTriggerExit2D(Collider2D someone){
        if (someone.CompareTag("Player")){    
            Player player = someone.GetComponent<Player>();
            if (playersToAttack.Contains(player))
            {
                playersToAttack.Remove(player);
            }
        }
        if (someone.CompareTag("FriskCopy")){  
            friskCopy=false;
        }
        turnOf.attackButton.interactable=(playersToAttack.Count > 0||friskCopy) && turnOf.oneAttack;
    }
    public void HabSound(){
        audioSource.PlayOneShot(audioClips[3]);
    }
}