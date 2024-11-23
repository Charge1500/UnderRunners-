using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Stats
    public string playerName;
    public int health;
    public int attack;
    public int speed;
    public bool isTurn = false;
    public Ability uniqueAbility;

    // Movement
    public float speedMovement = 5f;
    private Vector2 _movement;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Animator
    private Animator animator;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Solo permitir movimiento si es el turno del jugador
        if (isTurn)
        {
            // Movimiento
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // ANIMACIONES
            // Personaje si esta caminando o no
            if(moveHorizontal != 0 || moveVertical != 0){
                animator.SetBool("IsWalking", true);
            } else {
                animator.SetBool("IsWalking", false);
            }

            // Determinar la dirección del movimiento
            if (moveHorizontal != 0) {
                animator.SetInteger("WalkDirection", 2); // Horizontal
                // Ajustar la escala del sprite para caminar a la derecha o izquierda
                Vector3 playerScale = transform.localScale;
                playerScale.x = moveHorizontal < 0 ? 1 : -1;
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

            // Aplicar la velocidad y deltaTime
            _movement *= speedMovement * Time.deltaTime;
        }
        else
        {
            // Si no es el turno del jugador, resetear el movimiento
            _movement = Vector2.zero;
        }
    }

    void FixedUpdate() {
        // Mover el Rigidbody2D
        if (isTurn) {
            rb.MovePosition(rb.position + _movement);
        }
        // Cambiar el orden en la capa dependiendo de su posición en y
        sr.sortingOrder = (int)rb.position.y * -1;
    }
}
