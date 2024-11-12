using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // Prefab para las paredes
    public GameObject pathPrefab; // Prefab para los caminos

    public int width = 11;
    public int height = 11;
    private int[,] maze; // Matriz para almacenar las celdas

    // Start is called before the first frame update
    void Start()
    {
        maze = new int[height,width];
        
        // Inicializa el laberinto con muros
        InitialMaze();

        maze[0, 0] = 0; // Celda inicial como camino
        GenerateMaze(0, 0);

        //Instancia cada elemento del laberinto
        DrawMaze();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Inicializa el laberinto con muros
    void InitialMaze(){
        for (int i = 0; i < height; i++){
            for (int j = 0; j < width; j++){
                maze[i, j] = 1; // Llenar con muros
            }
        }
    }

    void GenerateMaze(int x,int y)
    {
        
        // Direcciones posibles: arriba, abajo, izquierda, derecha
        int[] directionInX = { -1, 1, 0, 0 };
        int[] directionInY = { 0, 0, -1, 1};

        int[] possibleDirections={0, 1, 2, 3};
        Mezclar(possibleDirections);
    
        foreach (int direction in possibleDirections){

            int neighborX = x + (directionInX[direction] * 2);
            int neighborY = y + (directionInY[direction] * 2);

            //Garantizando que no se salga de la matriz el proximo elemento y que sea un muro
            if (neighborX >= 0 && neighborY >= 0 && neighborX < height && neighborY < width && maze[neighborX, neighborY] == 1)
            {
                // "Rompe la pared" entre la celda actual y de la que salio
                maze[neighborX, neighborY] = 0; // Marca como camino
                maze[x + directionInX[direction], y + directionInY[direction]] = 0; // Conectar caminos

                // Llama recursivamente a la actual
                GenerateMaze(neighborX, neighborY);
            }
               
        }
         
    }
    void Mezclar(int[] directions){
        int aux;

        for(int i=directions.Length-1;i>0;i-- ){
            // Elige una posicion aleatoria entre 0 e i
            int j = Random.Range(0, i + 1);

            aux = directions[i];
            directions[i] = directions[j];
            directions[j] = aux;
        }
    }

    void DrawMaze()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (maze[i, j] == 1)
                {
                    Instantiate(wallPrefab, new Vector3(i, j), Quaternion.identity);
                    
                }
                if (maze[i, j] == 0)
                {
                    Instantiate(pathPrefab, new Vector3(i, j), Quaternion.identity);
                }
            }
        }
    } 
}
