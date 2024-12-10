using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTrap : Traps
{
    void OnCollisionEnter2D(Collision2D someone)
    {
        if (someone.gameObject.CompareTag("Player"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("ExplosiveExplosion") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("BombActive"))
            {
                Player player = playerCollider.GetComponent<Player>();
                player.TakeDamage(2);
                StartCoroutine(DestroyAfterAnimation());
            }
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Esperar hasta que la animación termine
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Destruir el objeto después de la animación
        Destroy(gameObject);
    }
}
