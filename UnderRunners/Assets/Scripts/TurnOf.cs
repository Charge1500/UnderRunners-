using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnOf : MonoBehaviour
{
    private Pencil pencil;
    public List<Player> turns = new List<Player>();
    public int currentTurnIndex = 0;
    public float turnDuration = 10f; // Duración del turno en segundos
    private float turnTimer; // Temporizador del turno
    public TMP_Text turnTimerText; // Referencia al texto de UI para el temporizador

    public int puntuationToWin=5;

    void Awake()
    {
        pencil = GetComponent<Pencil>();
    }
    void Update(){
        if (turnTimer > 0)
        {
            turnTimer -= Time.deltaTime; // Reducir el temporizador en función del tiempo transcurrido
            turnTimerText.text = Mathf.Ceil(turnTimer).ToString(); // Actualizar el texto del temporizador solo con los segundos
        }else
        {
            NextTurn();
        }
    }
    public void AssignPlayerOrder()
    {
        turns.Clear();
        if (pencil.player1 != null)
        {
            turns.Add(pencil.player1.GetComponent<Player>());
            turns[0].isTurn = false;
        }
        if (pencil.player2 != null)
        {
            turns.Add(pencil.player2.GetComponent<Player>());
            turns[1].isTurn = false;
        }
        if (pencil.player3 != null)
        {
            turns.Add(pencil.player3.GetComponent<Player>());
            turns[2].isTurn = false;
        }
        if (pencil.player4 != null)
        {
            turns.Add(pencil.player4.GetComponent<Player>());
            turns[3].isTurn = false;
        }
    }

    public void StartTurn()
    {
        turns[currentTurnIndex].isTurn = true; // Comienza el turno del primer jugador
        turnTimer = turnDuration; // Restablecer el temporizador
    }


    public void NextTurn()
    {
        CheckWin();
        turns[currentTurnIndex].isTurn = false; // Termina el turno del jugador actual
        turns[currentTurnIndex].RestoreOriginalStats();
        currentTurnIndex = (currentTurnIndex + 1) % turns.Count; // Pasa al siguiente jugador
        StartTurn(); // Comienza el turno del siguiente jugador
    }

    public void IncreaseTurnTime(float additionalTime){ 
        turnTimer += additionalTime; 
    }
    public void CheckWin(){
        if(turns[currentTurnIndex].hasRuby){
            turns[currentTurnIndex].puntuation+=1;
        }
        if(turns[currentTurnIndex].puntuation==puntuationToWin){
            Debug.Log("You Win");
        }
    }
}
