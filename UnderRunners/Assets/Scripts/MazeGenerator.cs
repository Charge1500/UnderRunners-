using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int height = 0;
    public int width = 0;
    private int[,] maze;

    void Start(){
        width=GameData.Instance.size;
        height=GameData.Instance.size;
    }
    public void InitializeMaze()
    {
        width+=(width % 2 == 0) ? 1 : 0;
        height+=(height % 2 == 0) ? 1 : 0;
        maze = new int[width, height];
        InitialMaze();
        maze[1, 1] = 0;
        GenerateMaze(1, 1);
        OpenPaths();
    }

    public void InitialMaze()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i, j] = 1;
            }
        }
    }

    public void GenerateMaze(int x, int y)
    {
        int[] directionInX = { -1, 1, 0, 0 };
        int[] directionInY = { 0, 0, -1, 1 };
        int[] possibleDirections = { 0, 1, 2, 3 };
        Shuffle(possibleDirections);

        foreach (int direction in possibleDirections)
        {
            int neighborX = x + (directionInX[direction] * 2);
            int neighborY = y + (directionInY[direction] * 2);

            if (neighborX >= 0 && neighborY >= 0 && neighborX < width && neighborY < height && maze[neighborX, neighborY] == 1)
            {
                maze[neighborX, neighborY] = 0;
                maze[x + directionInX[direction], y + directionInY[direction]] = 0;
                GenerateMaze(neighborX, neighborY);
            }
        }
    }

    public void OpenPaths()
    {
        List<(int, int)> muros = new List<(int, int)>();
        for (int i = 1; i < width - 1; i++)
        {
            for (int j = 1; j < height - 1; j++)
            {
                if (maze[i, j] == 1 && !((maze[i + 1, j] == 1 && (maze[i, j + 1] == 1 || maze[i, j - 1] == 1)) || (maze[i - 1, j] == 1 && (maze[i, j + 1] == 1 || maze[i, j - 1] == 1))))
                {
                    muros.Add((i, j));
                }
            }
        }

        int cantidadAConvertir = Mathf.FloorToInt(muros.Count * 20 / 100);
        for (int k = 0; k < cantidadAConvertir; k++)
        {
            int index = Random.Range(0, muros.Count);
            (int x, int y) = muros[index];
            maze[x, y] = 0;
            muros.RemoveAt(index);
        }
    }

    public int[,] GetMaze()
    {
        return maze;
    }

    public void Shuffle(int[] directions)
    {
        int aux;
        for (int i = directions.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            aux = directions[i];
            directions[i] = directions[j];
            directions[j] = aux;
        }
    }

    public List<(int, int)> Paths()
    {
        List<(int, int)> paths = new List<(int, int)>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (maze[i, j] == 0)
                {
                    paths.Add((i, j));
                }
            }
        }
        paths.Remove((1,1));
        paths.Remove((width-2,height-2));
        paths.Remove((width-2,1));
        paths.Remove((1,height-2));
        return paths;
    }
}
