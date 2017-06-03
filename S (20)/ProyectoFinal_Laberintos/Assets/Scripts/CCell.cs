using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyDataStructures.Graphs;

public class CCell : MonoBehaviour
{
    public GameObject[] walls;
    public int row { get; private set; }
    public int col { get; private set; }
    public Vertex<CCell> myVertex;
    public bool visited;
    public int distance;

    protected Text text;


    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        distance = int.MaxValue - 1;
    }

    private void OnMouseUp()
    {
        CMazeManager.instance.SetCell(this);
    }

    public void Init(int r, int c)
    {
        row = r;
        col = c;
    }

    public void Visit(int d)
    {
        visited = true;
        if (distance > d) distance = d;
        text.text = distance.ToString();
    }

    public void EnableWalls()
    {
        foreach (GameObject go in walls) go.SetActive(true);
    }

    public void DisableWall(int index)
    {
        walls[index].SetActive(false);
    }
}
