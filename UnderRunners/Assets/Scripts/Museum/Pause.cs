using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public GameObject player;
    public GameObject menuPanel;
    public GameObject resumir;
    // Cámara
    public Camera mainCamera;
    public float cameraFollowSpeed = 5f;
    public float cameraZoom = 10f;
    private float originalCameraZoom;

     void Start(){
        originalCameraZoom = mainCamera.orthographicSize;
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
        FollowCurrentPlayer();
    }

    public void Resume(){
        Time.timeScale = 1f; 
        menuPanel.SetActive(false);
    }
    public void Reiniciar(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Museo");
    }

    public void Menu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void Salir(){
        Application.Quit();
    }

    private void FollowCurrentPlayer(){
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * cameraFollowSpeed);

        // Ajustar el zoom
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, cameraZoom, Time.deltaTime * cameraFollowSpeed);
    }
    private void ResetCameraZoom(){
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, originalCameraZoom, Time.deltaTime * cameraFollowSpeed);
    }
}
