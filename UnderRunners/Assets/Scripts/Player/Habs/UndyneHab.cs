using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndyneHab : Habs
{
    public void OnTriggerEnter2D(Collider2D someone)
    {
        if (someone.gameObject.CompareTag("Player"))
        {
            Player player = someone.gameObject.GetComponent<Player>();
            player.TakeDamage(4);
        }
    }
}
