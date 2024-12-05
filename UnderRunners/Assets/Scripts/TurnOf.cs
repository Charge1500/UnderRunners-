using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOf : MonoBehaviour
{
    private Pencil pencil;
    public List<Player> turns = new List<Player>();
    public int currentTurnIndex = 0;
    public int stepsCounter = 0;
    public Button endTurnButton; // Referencia al botón de finalizar turno

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
        turns[currentTurnIndex].isTurn = true;  // Comienza el turno del primer jugador
        
        // Deshabilitar el botón al inicio del turno
        endTurnButton.interactable = false;
    }

    public void NextTurn()
    {   
        turns[currentTurnIndex].isTurn = false;  // Termina el turno del jugador actual
        ResetInvisibleWalls(turns[currentTurnIndex].transform.position); // Reactivar los triggers
        currentTurnIndex = (currentTurnIndex + 1) % turns.Count;  // Pasa al siguiente jugador
        stepsCounter = 0;
        StartTurn();  // Comienza el turno del siguiente jugador
    }

    public void PlayerEnteredNewCell(Player player)
    {
        if (turns[currentTurnIndex] == player && player.isTurn)
        {
            stepsCounter++;
            if (stepsCounter >= player.speed)
            {    
                StartCoroutine(WaitAndEnableInvisibleWalls(player.transform.position));
            }
        }
    }

    private IEnumerator WaitAndEnableInvisibleWalls(Vector2 playerPosition)
    {
        yield return new WaitForSeconds(0.2f);

        EnableInvisibleWalls(playerPosition);
        
        // Habilitar el botón de finalizar turno
        endTurnButton.interactable = true;
    }

    private void EnableInvisibleWalls(Vector2 playerPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPosition, 1.5f); // Ajusta el radio según sea necesario
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Path"))
            {
                collider.isTrigger = false;
            }
        }
    }

    private void ResetInvisibleWalls(Vector2 playerPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPosition, 2f); // Ajusta el radio según sea necesario
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Path"))
            {
                collider.isTrigger = true; // Volver a activar los triggers
            }
        }
    }
}
