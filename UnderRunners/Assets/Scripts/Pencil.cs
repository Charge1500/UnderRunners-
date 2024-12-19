using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    private MazeGenerator mazeGenerator;

    public GameObject[] wallsPrefabs;
    public GameObject[] pathsPrefabs;
    public GameObject ruby;
    public Vector3 rubyRespawnPoint;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject playersParent;

    public GameObject[] trapPrefabs; 
    public GameObject[] consumablePrefabs;
    private int trapProbability = 1;
    private int consumableProbability = 2;
    
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
                    GameObject wallInstance;
                    if (i == 0 || i==width-1 || j==0 || j==height-1)
                    {
                        wallInstance = Instantiate(wallsPrefabs[0], new Vector3(i, j, 0), Quaternion.identity); 
                        wallInstance.transform.SetParent(playersParent.transform, true);
                    } else if (maze[i-1,j]==0 && maze[i+1,j]==0 && maze[i,j-1]==0 && maze[i,j+1]==0)
                    {
                        wallInstance = Instantiate(wallsPrefabs[Random.Range(1,4)], new Vector3(i, j, 0), Quaternion.identity); 
                        wallInstance.transform.SetParent(playersParent.transform, true);
                    } else if (maze[i-1,j]==0 && maze[i+1,j]==0 && maze[i,j-1]==0 && maze[i,j+1]==1)
                    {
                        wallInstance = Instantiate(wallsPrefabs[Random.Range(4,7)], new Vector3(i, j, 0), Quaternion.identity); 
                        wallInstance.transform.SetParent(playersParent.transform, true);
                    } else if (maze[i-1,j]==0 && maze[i+1,j]==1 && maze[i,j-1]==0)
                    {
                        wallInstance = Instantiate(wallsPrefabs[7], new Vector3(i, j, 0), Quaternion.identity); 
                        wallInstance.transform.SetParent(playersParent.transform, true);
                    } else if (maze[i+1,j]==0 && maze[i-1,j]==1 && maze[i,j-1]==0)
                    {
                        wallInstance = Instantiate(wallsPrefabs[8], new Vector3(i, j, 0), Quaternion.identity); 
                        wallInstance.transform.SetParent(playersParent.transform, true);
                    } else if (maze[i+1,j]==1 && maze[i-1,j]==1 && maze[i,j-1]==0)
                    {
                        wallInstance = Instantiate(wallsPrefabs[Random.Range(9,13)], new Vector3(i, j, 0), Quaternion.identity); 
                        wallInstance.transform.SetParent(playersParent.transform, true);
                    } else if (maze[i+1,j]==1 && maze[i-1,j]==0 && maze[i,j+1]==0)
                    {
                        wallInstance = Instantiate(wallsPrefabs[13], new Vector3(i, j, 0), Quaternion.identity); 
                        wallInstance.transform.SetParent(playersParent.transform, true);
                    }else if (maze[i+1,j]==0 && maze[i-1,j]==1 && maze[i,j+1]==0)
                    {
                        wallInstance = Instantiate(wallsPrefabs[14], new Vector3(i, j, 0), Quaternion.identity); 
                        wallInstance.transform.SetParent(playersParent.transform, true);
                    }else if (maze[i+1,j]==0 && maze[i-1,j]==0 && maze[i,j+1]==0)
                    {
                        wallInstance = Instantiate(wallsPrefabs[15], new Vector3(i, j, 0), Quaternion.identity); 
                        wallInstance.transform.SetParent(playersParent.transform, true);
                    }else{
                    wallInstance = Instantiate(wallsPrefabs[0], new Vector3(i, j, 0), Quaternion.identity); 
                    wallInstance.transform.SetParent(playersParent.transform, true);
                    }
                }
                else if (maze[i, j] == 0)
                {
                    if(maze[i-1,j] == 1){
                        GameObject pathInstance = Instantiate(pathsPrefabs[1], new Vector3(i, j, 0), Quaternion.identity);
                        pathInstance.transform.SetParent(playersParent.transform, true);
                    } else{
                        GameObject pathInstance = Instantiate(pathsPrefabs[0], new Vector3(i, j, 0), Quaternion.identity);
                        pathInstance.transform.SetParent(playersParent.transform, true);
                    }
                }
            }
        }
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
            int width = mazeGenerator.width;
            int height = mazeGenerator.height;
            if(paths.Contains((width/2,height/2))){
                InstantiateRuby(new Vector3(width/2,height/2,0));
                paths.Remove((width/2,height/2));
                rubyRespawnPoint=new Vector3(width/2,height/2,0);
            } else if(paths.Contains((width/2+1,height/2))){
                InstantiateRuby(new Vector3(width/2+1,height/2,0));
                paths.Remove((width/2+1,height/2));
                rubyRespawnPoint=new Vector3(width/2+1,height/2,0);
            } else if(paths.Contains((width/2,height/2+1))){
                InstantiateRuby(new Vector3(width/2,height/2+1,0));
                paths.Remove((width/2,height/2+1));
                rubyRespawnPoint=new Vector3(width/2,height/2+1,0);
            } else if(paths.Contains((width/2-1,height/2))){
                InstantiateRuby(new Vector3(width/2-1,height/2,0));
                paths.Remove((width/2-1,height/2));
                rubyRespawnPoint=new Vector3(width/2-1,height/2,0);
            } else{
                InstantiateRuby(new Vector3(width/2,height/2-1,0));
                paths.Remove((width/2,height/2-1));
                rubyRespawnPoint=new Vector3(width/2,height/2-1,0);
            }
        List<(int, int)> deletedPaths = new List<(int, int)>();
        bool wasOcuped=false;
        
        foreach ((int x, int y) in paths)
        {  
            if(!wasOcuped && !deletedPaths.Contains((x-1,y))){
                int randomValue = Random.Range(1, 6);
                
                if (randomValue == consumableProbability)
                { 
                    InstantiateRandomPrefab(consumablePrefabs, new Vector3(x, y, 0));
                    deletedPaths.Add((x,y));
                    wasOcuped=true;
                    continue;
                }
                else if (randomValue == trapProbability)
                {
                    InstantiateRandomPrefab(trapPrefabs, new Vector3(x, y, 0));
                    deletedPaths.Add((x,y));
                    wasOcuped=true;
                    continue;
                }
            }
            wasOcuped=false;
        }
        
    }

    private void InstantiateRandomPrefab(GameObject[] prefabs, Vector3 position)
    {
        int index = Random.Range(0, prefabs.Length);
        GameObject prefabInstance = Instantiate(prefabs[index], position, Quaternion.identity);
        prefabInstance.transform.SetParent(playersParent.transform, true);
    }

    public void InstantiateRuby(Vector3 position)
    {
        GameObject rubyInstance = Instantiate(ruby, position, Quaternion.identity);
        rubyInstance.transform.SetParent(playersParent.transform, true);
    }
}
