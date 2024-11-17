using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    //Stats
    public string playerName;
    public int health;
    public int attack;
    public int speed;
    public bool isTurn=false;
    public Ability uniqueAbility;

    //Movement
    public float speedMovement = 2.5f;
    private Vector2 _movement;


    void Update(){
    }
}
