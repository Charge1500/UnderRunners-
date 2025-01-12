using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
public class GameManager : MonoBehaviour
{
    private MazeGenerator mazeGenerator;
    private Pencil pencil;
    private TurnOf turnOf;
    public GameObject menuPanel;
    public GameObject resumir;

    void Awake()
    {
        mazeGenerator = GetComponent<MazeGenerator>();
        pencil = GetComponent<Pencil>();
        turnOf = GetComponent<TurnOf>(); 
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
        pencil.GenerateTrapsAndConsumables();

        //orden de los jugadores y comenzar el primer turno
        turnOf.AssignPlayerOrder();
        turnOf.StartTurn();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
        if (menuPanel.activeSelf) { 
            Time.timeScale = 0f; // Pausa el juego 
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumir);
        } else { 
            Time.timeScale = 1f; // Reanuda el juego 
        }
        if(turnOf.gameEnded){
            Time.timeScale = 0f;
        }
    }

    public void Resume(){
        Time.timeScale = 1f; 
        menuPanel.SetActive(false);
    }
    public void Reiniciar(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void Menu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void Salir(){
        Application.Quit();
    }
}


