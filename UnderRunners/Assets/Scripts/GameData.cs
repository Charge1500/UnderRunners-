using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public GameObject[] selectedPlayers;
    public int turnTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Este objeto persiste entre escenas.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetData()
    {
        selectedPlayers = null; // Reinicia jugadores seleccionados
        turnTime = 0;           // Reinicia el tiempo de turno
    }

}

