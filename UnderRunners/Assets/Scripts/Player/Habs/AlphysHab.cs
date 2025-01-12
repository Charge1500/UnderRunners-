using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphysHab : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.stun = true;
            player.TakeDamage(1);
        }
    }
}
