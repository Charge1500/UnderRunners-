using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMuseum : MonoBehaviour
{
    // Movement
    public float speedMovement = 3f;
    public float currentSpeed;
    private Vector2 _movement;
    private Rigidbody2D rb;
    public SpriteRenderer sr;

    // Animator
    public Animator animator;

    public TurnOf turnOf;
    private Pencil pencil;

    void Awake(){

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        currentSpeed=speedMovement;
    }
    void Update()
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

    void FixedUpdate() { 
        rb.MovePosition(rb.position + _movement);  
        sr.sortingOrder = (int)rb.position.y * -1;
    }
}