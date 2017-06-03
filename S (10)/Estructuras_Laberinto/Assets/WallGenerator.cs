using System.IO;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Sebastian Arriola
/// David del Aguila
/// Matthew Kummerfeldt
/// </summary>
public class WallGenerator : MonoBehaviour
{
    public GameObject[] walls; // los objetos de juego que forman el laberinto
    public string fileName; // nombre del archivo a donde se lee y escribe el laberinto
    public Transform firstWallPos; // posicion de referencia para instanciar la primer pared
    public float wallOffset; // distancia entre objetos de juego del laberinto

    private Vector3 nextWallPos; // posicion del siguiente objeto del laberinto
    private List<GameObject> currLab; // lista que guarda los objetos del laberinto actual
    private Dictionary<int, GameObject> gameObjectDic; // diccionario que mapea numeros a objetos del laberinto

    private void Start()
    {
        // se guarda la posicion del siguiente objeto del laberinto
        nextWallPos = firstWallPos.position;
        // se instancia lista y diccionario
        currLab = new List<GameObject>();
        gameObjectDic = new Dictionary<int, GameObject>();
        // se llena el diccionario con los objetos de juego y numeros
        FillDictionary();
        // se carga del archivo de texto el laberinto
        LoadWalls(fileName);
    }

    /// <summary>
    /// Lee el archivo de texto y instancia los objetos de juego del laberinto
    /// </summary>
    /// <param name="fileName"></param>
    private void LoadWalls(string fileName)
    {
        string[] arr = File.ReadAllLines(fileName);
        foreach (string s in arr)
        {
            string[] line = s.Split(' ');
            foreach (string wall in line)
            {
                int n = int.Parse(wall);
                GameObject go = Instantiate(gameObjectDic[n], nextWallPos, Quaternion.identity);
                currLab.Add(go);
                nextWallPos = new Vector3(nextWallPos.x + wallOffset, nextWallPos.y, 0f);
            }
            nextWallPos = new Vector3(firstWallPos.position.x, nextWallPos.y - wallOffset, 0f);
        }
    }

    /// <summary>
    /// Genera una secuencia de numeros psedo random que representan
    /// un laberinto y escribe esta secuencia a un archivo de texto.
    /// </summary>
    public void GenerarLaberintoRandom()
    {
        // destruir todos los objetos en currLab
        foreach (GameObject g in currLab)
            Destroy(g);
        currLab.Clear();
        // resetear posicion primer wall
        nextWallPos = firstWallPos.position;
        StreamWriter sw = File.CreateText(fileName);
        for(int row = 0; row < 8; row++)
        {
            for(int col = 0; col < 8; col++)
            {
                if (col < 7)
                    sw.Write(Random.Range(0, walls.Length) + " ");
                else if(col == 7)
                    sw.Write(Random.Range(0, walls.Length));
            }
            sw.WriteLine();
        }
        sw.Flush();
        sw.Close();
        LoadWalls(fileName); // load the new laberinth
    }

    /// <summary>
    /// Llena el diccionario con numero y objeto de juego.
    /// Mapea un numero i a un objeto de juego.
    /// </summary>
    private void FillDictionary()
    {
        for(int i = 0; i < walls.Length; i++)
        {
            gameObjectDic.Add(i, walls[i]);
        }
    }
}
