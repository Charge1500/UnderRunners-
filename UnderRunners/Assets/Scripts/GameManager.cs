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
    public AudioClip track;

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

        // Llamar a Pencil para dibujar
        pencil.DrawMaze();
        pencil.DrawPlayers();
        pencil.GenerateTrapsAndConsumables();

        //orden de los jugadores y comenzar el primer turno
        turnOf.AssignPlayerOrder();
        turnOf.StartTurn();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Q)){
            turnOf.Attack();
        }
        if(Input.GetKeyDown(KeyCode.E)){
            if(!turnOf.isWaitingForAbilityClick){
                turnOf.turns[turnOf.currentTurnIndex].ActiveHab();
            }else if(turnOf.isWaitingForAbilityClick){
                turnOf.isWaitingForAbilityClick=false;
            }
        }
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
        if(turnOf.endTurnScreen.activeSelf){
           if(Input.GetKeyDown(KeyCode.Return)){
                Aceptar();
            }
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("UnderMaze");
        MusicManager.Instance.ChangeTrack(track);
    }

    public void Menu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void Salir(){
        Application.Quit();
    }

    public void Aceptar(){
        Time.timeScale = 1f; 
        turnOf.endTurnScreen.SetActive(false);
        turnOf.NextNextTurn();
    }
}