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

        List<(int, int)> paths1 = new List<(int, int)>();
        List<(int, int)> paths2 = new List<(int, int)>();

        if(player1!=null&&player2!=null){
            for (int i = 1; i < width - 1; i++)
            {
                if (maze[i, 1] == 0)
                {
                    paths1.Add((i, 1));
                }
                if (maze[i, height - 2] == 0)
                {
                    paths2.Add((i, height - 2));
                }
            }

            int index1 = Random.Range(0, paths1.Count);
            int index2 = Random.Range(0, paths2.Count);

            (int x, int y) = paths1[index1];
           GameObject player1Instance = Instantiate(player1, new Vector3(x, y, 0), Quaternion.identity); 
           player1Instance.transform.SetParent(playersParent.transform, true);
            
            
            (x, y) = paths2[index2];
            GameObject player2Instance = Instantiate(player2, new Vector3(x, y, 0), Quaternion.identity); 
            player2Instance.transform.SetParent(playersParent.transform, true);
        }
    }
}
