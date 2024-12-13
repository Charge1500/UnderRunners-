using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    private MazeGenerator mazeGenerator;

    public GameObject wallPrefab;
    public GameObject pathPrefab;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject playersParent;

    public GameObject[] trapPrefabs; 
    public GameObject[] consumablePrefabs;
    public int trapProbability = 1;
    public int consumableProbability = 2;
    
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

        GenerateTrapsAndConsumables();
    }

    public void DrawPlayers()
    {
        int[,] maze = mazeGenerator.GetMaze();
        int width = mazeGenerator.width;
        int height = mazeGenerator.height;

        int x1 = 1;
        int y1 = 1;
        if (maze[x1, y1] == 0)
        {
            GameObject player1Instance = Instantiate(player1, new Vector3(x1, y1, 0), Quaternion.identity);
            player1Instance.GetComponent<Player>().respawnPoint = new Vector2(x1, y1);
            player1Instance.transform.SetParent(playersParent.transform, true);
            player1 = player1Instance;
        }

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

    public void GenerateTrapsAndConsumables()
    { 
        List<(int, int)> paths = mazeGenerator.Paths();

        foreach ((int x, int y) in paths)
        {
            
            int randomValue = Random.Range(1, 6);
            
            if (randomValue == consumableProbability)
            {
                InstantiateRandomPrefab(consumablePrefabs, new Vector3(x, y, 0));
            }
            else if (randomValue == trapProbability)
            {
                InstantiateRandomPrefab(trapPrefabs, new Vector3(x, y, 0));
            }
        }
    }

    void InstantiateRandomPrefab(GameObject[] prefabs, Vector3 position)
    {
        Debug.Log("Hola");
        int index = Random.Range(0, prefabs.Length);
        GameObject prefabInstance = Instantiate(prefabs[index], position, Quaternion.identity);
        prefabInstance.transform.SetParent(playersParent.transform, true);
    }
}
