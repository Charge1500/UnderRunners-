using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    public static Pencil instance;
    private MazeGenerator mazeGenerator;

    public GameObject wallPrefab;
    public GameObject pathPrefab;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject playersParent;
    
    void Awake()
    {
        mazeGenerator = GetComponent<MazeGenerator>();
    }

    public void DrawMaze()
    {
        int[,] maze = mazeGenerator.GetMaze();
        int width = mazeGenerator.width;
        int height = mazeGenerator.height;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (maze[i, j] == 1)
                {
                    GameObject wallInstance = Instantiate(wallPrefab, new Vector3(i, j, 0), Quaternion.identity); 
                    wallInstance.transform.SetParent(playersParent.transform, true);
                }
                else if (maze[i, j] == 0)
                {
                    GameObject pathInstance = Instantiate(pathPrefab, new Vector3(i, j, 0), Quaternion.identity); 
                    pathInstance.transform.SetParent(playersParent.transform, true);
                }
            }
        }
    }

    public void DrawPlayers()
    {
        int[,] maze = mazeGenerator.GetMaze();
        int width = mazeGenerator.width;
        int height = mazeGenerator.height;

        // Fijar el punto de respawn del jugador 1
        int x1 = 1;
        int y1 = 1;
        if (maze[x1, y1] == 0)
        {
            GameObject player1Instance = Instantiate(player1, new Vector3(x1, y1, 0), Quaternion.identity);
            player1Instance.GetComponent<Player>().respawnPoint = new Vector2(x1, y1);
            player1Instance.transform.SetParent(playersParent.transform, true);
            player1 = player1Instance;
        }

        // Fijar el punto de respawn del jugador 2
        int x2 = width - 2;
        int y2 = height - 2;
        if (maze[x2, y2] == 0)
        {
            GameObject player2Instance = Instantiate(player2, new Vector3(x2, y2, 0), Quaternion.identity);
            player2Instance.GetComponent<Player>().respawnPoint = new Vector2(x2, y2);
            player2Instance.transform.SetParent(playersParent.transform, true);
            player2 = player2Instance;
        }
    }


}
