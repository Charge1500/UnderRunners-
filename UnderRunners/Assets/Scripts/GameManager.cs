using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private MazeGenerator mazeGenerator;
    private Pencil pencil;
    public TurnOf turnOf;  // Añadido: Referencia al script TurnOf

    void Awake()
    {
        mazeGenerator = GetComponent<MazeGenerator>();
        pencil = GetComponent<Pencil>();
        turnOf = GetComponent<TurnOf>();  // Añadido: Inicialización del script TurnOf
    }

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        // Inicializar el laberinto
        mazeGenerator.InitializeMaze();
        mazeGenerator.GenerateMaze(1, 1);
        mazeGenerator.OpenPaths();

        // Llamar a Pencil para dibujar
        pencil.DrawMaze();
        pencil.DrawPlayers();

        // Añadido: Inicializar el orden de los jugadores y comenzar el primer turno
        turnOf.AssignPlayerOrder();
        turnOf.StartTurn();
    }

    void Update()
    {

    }
}
