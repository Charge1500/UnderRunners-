using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansMove : MonoBehaviour
{
   

    // Animator
    private Animator animator; // Stats

    // Movement
    private Vector2 _movement;
    public float speedMovement = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

            // Movimiento
            float moveHorizontal = Input.GetAxis("Horizontal");

            // ANIMACIONES
            if(moveHorizontal != 0){
                animator.SetBool("IsWalking", true);
                Vector3 playerScale = transform.localScale;
                playerScale.x = Mathf.Abs(playerScale.x) * (moveHorizontal < 0 ? -1 : 1);
                transform.localScale = playerScale;
            } else {
                animator.SetBool("IsWalking", false);
            }

            // Calcular el movimiento
            _movement = new Vector2(moveHorizontal, 0);

            // Normalizar el vector de movimiento si su magnitud es mayor que 1
            if (_movement.magnitude > 1)
            {
                _movement.Normalize();
            }

            _movement *= speedMovement * Time.deltaTime;
    }

    void FixedUpdate() {
            rb.MovePosition(rb.position + _movement);
    }
}
