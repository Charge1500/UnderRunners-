using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // Prefab para las muros
    public GameObject pathPrefab; // Prefab para los caminos

    public GameObject player1;
    public GameObject player2;

    public int height = 11;
    public int width = 11;
    private int[,] maze; // Matriz para almacenar las celdas

    // Start is called before the first frame update
    void Start()
    {
        maze = new int[width,height];
        
        // Inicializa el laberinto con muros
        InitialMaze();

        maze[1, 1] = 0; // Celda inicial como camino
        GenerateMaze(1, 1);
        OpenPaths();

        //Instancia cada elemento del laberinto
        DrawMaze();
        DrawPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Inicializa el laberinto con muros
    void InitialMaze(){
        for (int i = 0; i < width; i++){
            for (int j = 0; j < height; j++){
                maze[i, j] = 1; // Llenar con muros
            }
        }
    }

    public void GenerateMaze(int x,int y)
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
            if (neighborX >= 0 && neighborY >= 0 && neighborX < width && neighborY < height && maze[neighborX, neighborY] == 1)
            {
                // "Rompe la muro" entre la celda actual y de la que salio
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

    public void OpenPaths(){
        // Lista de posiciones de muros como pares de índices (i, j)
        List<(int, int)> muros = new List<(int, int)>();

        // Identificar las posiciones de las muros
        for (int i = 1; i < width-1; i++)
        {
            for (int j = 1; j < height-1; j++)
            {
                if (maze[i, j] == 1 &&   !((maze[i+1,j]==1 && (maze[i,j+1]==1 || maze[i,j-1]==1))     ||     (maze[i-1,j]==1 && (maze[i,j+1]==1 || maze[i,j-1]==1))))
                {
                    muros.Add((i, j)); // Guardar la posición de la muro
                }
            }
        }

        // Determinar cuántas muros convertir en caminos
        int cantidadAConvertir = Mathf.FloorToInt(muros.Count * 30 / 100);

        // Seleccionar muros aleatoriamente
        for (int k = 0; k < cantidadAConvertir; k++)
        {
            int index = Random.Range(0,muros.Count);
            (int x, int y) = muros[index];
            maze[x, y] = 0; // Convertir a camino
            muros.RemoveAt(index); // Eliminar de la lista para evitar duplicados
        }
    }

    

    public void DrawMaze()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
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
    void DrawPlayers(){

        int randomPath=0;

        while(maze[randomPath,1]!=0){
            
            randomPath=Random.Range(1,width/2);

            if(maze[randomPath,1]==0){
                Instantiate(player1, new Vector3(randomPath, height-2), Quaternion.identity);
            }
        }

        randomPath=0;
        
        while(maze[randomPath,height-1]!=0){

            randomPath=Random.Range(width/2,width-1);
            
            if(maze[randomPath,height-1]==0){
                Instantiate(player2, new Vector3(randomPath, 1), Quaternion.identity);
            }
        }
    }
}
