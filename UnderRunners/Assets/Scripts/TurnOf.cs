using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOf : MonoBehaviour
{
    private Pencil pencil;
    public List<Player> turns = new List<Player>();
    public int currentTurnIndex = 0;
    public int stepsCounter = 0;

    void Awake()
    {
        pencil = GetComponent<Pencil>();
    }

    public void AssignPlayerOrder()
    {
        turns.Clear();  
        if (pencil.player1 != null)
        {
            turns.Add(pencil.player1.GetComponent<Player>());
        }
        if (pencil.player2 != null)
        {
            turns.Add(pencil.player2.GetComponent<Player>());
        }
        if (pencil.player3 != null)
        {
            turns.Add(pencil.player3.GetComponent<Player>());
        }
        if (pencil.player4 != null)
        {
            turns.Add(pencil.player4.GetComponent<Player>());
        }
    }

    public void StartTurn()
    {     
        currentTurnIndex=0;
        turns[currentTurnIndex].isTurn = true;  // Comienza el turno del primer jugador
    }

    public void NextTurn()
    {
        turns[currentTurnIndex].isTurn = false;  // Termina el turno del jugador actual
        currentTurnIndex = (currentTurnIndex + 1) % turns.Count;  // Pasa al siguiente jugador
        turns[currentTurnIndex].isTurn = true;  // Comienza el turno del siguiente jugador
    }

    public void PlayerEnteredNewCell(Player player)
    {
        if (turns[currentTurnIndex] == player && player.isTurn)
        {
            stepsCounter++;
            if (stepsCounter >= player.speed)
            {
                stepsCounter = 0;
                NextTurn();
            }
        }
    }
}
