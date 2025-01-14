using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Stats
    public Sprite imageDialogDefault;
    public Sprite image;
    public string playerName;
    public int health;
    public int currentHealth;
    public int attack;
    public int currentAttack;
    public int coldown;
    public int currentColdown;
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

    void Awake(){

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

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

            _movement *= currentSpeed * Time.deltaTime;
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

    private IEnumerator DamageEffect()
    {
        Color originalColor = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sr.color = originalColor;
    }

    private IEnumerator Die()
    {
        stun=false;
        bool wasTurn=isTurn;
        isTurn=false;
        animator.SetBool("ExcludeDead",false);
        animator.SetTrigger("Dead"); 
        yield return new WaitForSeconds(0.6f);
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
    transform.position = respawnPoint;
    currentHealth = health;
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
        isUsingHab = true;
        animator.SetBool("IsWalking", false);
        animator.SetInteger("WalkDirection", 1); // Abajo
        yield return new WaitForSeconds(habDuration); // Tiempo que tarda la habilidad en ejecutarse
        isUsingHab = false; // Reactiva el movimiento
    }

    private void OnTriggerEnter2D(Collider2D someone){
        if (someone.CompareTag("Player")){   
            Player player = someone.GetComponent<Player>();
            if (!playersToAttack.Contains(player))
            {
                playersToAttack.Add(player);
                turnOf.attackButton.interactable = true;
            }
        }
        if (someone.CompareTag("FriskCopy")){ 
            Debug.Log("DETECTADO");   
            friskCopy=true;
            turnOf.attackButton.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D someone){
        if (someone.CompareTag("Player")){    
            Player player = someone.GetComponent<Player>();
            if (playersToAttack.Contains(player))
            {
                playersToAttack.Remove(player);
                turnOf.attackButton.interactable = playersToAttack.Count > 0;
            }
        }
        if (someone.CompareTag("FriskCopy")){  
            Debug.Log("DETECTADO Salida");  
            friskCopy=false;
            turnOf.attackButton.interactable = friskCopy || playersToAttack.Count > 0;
        }
    }
}