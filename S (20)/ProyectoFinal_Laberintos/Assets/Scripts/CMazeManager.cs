using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyDataStructures.Graphs;

public class CMazeManager : MonoBehaviour
{
    public static CMazeManager instance;
    public GameObject cellPrefab;
    public GameObject nodePrefab;
    public GameObject edgePrefab;
    public GameObject cellOutlinePrefab;
    public Transform mazePos;
    public Transform graphPos;
    public float cellOffset;
    public float nodeOffset;
    public float animationSpeed;
    public Text infoText;
    public Slider animationSpeedSlider;
    public bool processRunning;

    protected Graph<CCell> graph;
    protected List<CCell> mazeGameO;
    protected List<CVertex> graphGameO;
    protected List<GameObject> edgeGameO;
    protected List<GameObject> pathEdgesGameO;
    protected int mazeSize;
    protected WaitForSeconds animationWait;
    protected CCell cellA;
    protected CCell cellB;
    protected GameObject cellOutlineA;
    protected GameObject cellOutlineB;
    protected Vertex<CCell> vertexA;
    protected Vertex<CCell> vertexB;
    protected bool numerationCompleted; // flag to indicate when the numeration process is completed

    private void Awake()
    {
        if (instance == null) instance = this;
        graph = new Graph<CCell>();
        mazeGameO = new List<CCell>();
        graphGameO = new List<CVertex>();
        edgeGameO = new List<GameObject>();
        pathEdgesGameO = new List<GameObject>();
        animationWait = new WaitForSeconds(animationSpeed);
        cellA = cellB = null;
    }

    #region Public Methods
    public void SetMazeSize(string s) { int.TryParse(s, out mazeSize); }

    public void SetAnimationSpeed(float v)
    {
        Time.timeScale = v;
    }

    public void NewLaberinth()
    {
        if (mazeSize > 1)
        {
            NewMazePreparations();
            mazePos.position = new Vector3(mazePos.position.x + cellOffset * mazeSize, mazePos.position.y);
            graphPos.position = new Vector3(graphPos.position.x - nodeOffset * mazeSize, mazePos.position.y);
            StartCoroutine(CreateNewGraph(mazeSize));
        }
    }

    public void SetCell(CCell c)
    {
        if (cellA == null)
        {
            cellA = c;
            if (cellOutlineA == null)
                cellOutlineA = Instantiate(cellOutlinePrefab, cellA.transform.position, cellOutlinePrefab.transform.rotation);
            else
            {
                cellOutlineA.transform.position = cellA.transform.position;
                cellOutlineA.SetActive(true);
            }
            vertexA = cellA.myVertex;
            infoText.text = string.Concat("CellA[", c.row, "," , c.col, "]");
        }
        else
        {
            cellB = c;
            if (cellOutlineB == null)
                cellOutlineB = Instantiate(cellOutlinePrefab, cellB.transform.position, cellOutlinePrefab.transform.rotation);
            else
            {
                cellOutlineB.SetActive(true);
                cellOutlineB.transform.position = cellB.transform.position;
            }
            vertexB = cellB.myVertex;
            infoText.text = infoText.text.Substring(0, 10);
            infoText.text += string.Concat(" ", "CellB[", c.row, ",", c.col, "]");
            foreach (GameObject g in pathEdgesGameO) Destroy(g);
        }
    }

    public void DijkstraNumeration()
    {
        if (vertexA != null)
        {
            DijkstraDistanceCalculation();
            numerationCompleted = true;
        }
    }

    public void DijkstraFindPath()
    {
        if(vertexA != null && vertexB != null)
            StartCoroutine(DijkstraFindPathCo());
    }

    public CCell GetCell(int row, int col)
    {
        int index = row * mazeSize + col;
        return mazeGameO[index];
    }

    public void SetVertexA(CCell c)
    {
        cellA = c;
        vertexA = cellA.myVertex;
        if(cellOutlineA == null)
            cellOutlineA = Instantiate(cellOutlinePrefab, cellA.transform.position, cellOutlinePrefab.transform.rotation);
        else
            cellOutlineA.transform.position = cellA.transform.position;
    }
    public void SetVertexB(CCell c)
    {
        cellB = c;
        vertexB = cellB.myVertex;
        if (cellOutlineB == null)
            cellOutlineB = Instantiate(cellOutlinePrefab, cellB.transform.position, cellOutlinePrefab.transform.rotation);
        else
            cellOutlineB.transform.position = cellB.transform.position;
    }

    public int GetMazeSize() { return mazeSize; }
    #endregion

    #region Protected Methods
    protected IEnumerator CreateNewGraph(int vertexCount)
    {
        processRunning = true;
        CCell newCell = null;
        CVertex newNode = null;
        Vector3 mPos = mazePos.position;
        Vector3 gPos = graphPos.position;
        int count = 0;
        for(int row = 0; row < mazeSize; row++)
        {
            mPos = new Vector3(mazePos.position.x, mazePos.position.y - row * cellOffset);
            gPos = new Vector3(graphPos.position.x, graphPos.position.y - row * nodeOffset);
            for (int col = 0; col < mazeSize; col++)
            {
                mPos = new Vector3(mazePos.position.x + col * cellOffset, mPos.y);
                gPos = new Vector3(graphPos.position.x + col * nodeOffset, gPos.y);
                newCell = Instantiate(cellPrefab, mPos, cellPrefab.transform.rotation).GetComponent<CCell>();
                newNode = Instantiate(nodePrefab, gPos, nodePrefab.transform.rotation).GetComponent<CVertex>();
                newNode.Init(count.ToString());
                newCell.Init(row, col);
                graph.InsertVertex(newCell);
                mazeGameO.Add(newCell);
                graphGameO.Add(newNode);
                count++;
                yield return animationWait;
            }
        }
        // reset position of drawing reference points
        mazePos.position = transform.position;
        graphPos.position = transform.position;
        StartCoroutine(GenerateMaze());
    }

    protected IEnumerator GenerateMaze()
    {
        int count, row = 0, col = 0, random;
        List<Vertex<CCell>> vertices = (List<Vertex<CCell>>)graph.Vertices();
        for(count = 0; count < graphGameO.Count; count++)
        {
            mazeGameO[count].myVertex = vertices[count];
            if (count == graphGameO.Count - 1) break;
            if (row == mazeSize - 1 && col < mazeSize - 1) // link east
            {
                graph.InsertEdge(vertices[count], vertices[count + 1]);
                DrawEdge(graphGameO[count].transform.position, graphGameO[count + 1].transform.position, 0);
                mazeGameO[count].DisableWall(1);
                mazeGameO[count + 1].DisableWall(3);
            }
            else if(col == mazeSize - 1 && row < mazeSize - 1) // link south
            {
                graph.InsertEdge(vertices[count], vertices[count + mazeSize]);
                DrawEdge(graphGameO[count].transform.position, graphGameO[count + mazeSize].transform.position, 0);
                mazeGameO[count].DisableWall(2);
                mazeGameO[count + mazeSize].DisableWall(0);
            }
            else // choose randomly between south and east
            {
                random = Random.Range(0, 2);
                if (random == 0)
                {
                    graph.InsertEdge(vertices[count], vertices[count + 1]);
                    DrawEdge(graphGameO[count].transform.position, graphGameO[count + 1].transform.position, 0);
                    mazeGameO[count].DisableWall(1);
                    mazeGameO[count + 1].DisableWall(3);
                }
                else
                {
                    graph.InsertEdge(vertices[count], vertices[count + mazeSize]);
                    DrawEdge(graphGameO[count].transform.position, graphGameO[count + mazeSize].transform.position, 0);
                    mazeGameO[count].DisableWall(2);
                    mazeGameO[count + mazeSize].DisableWall(0);
                }
            }
            col++;
            if(col == mazeSize)
            {
                col = 0;
                row++;
            }
            yield return animationWait;
        }
        processRunning = false;
    }

    protected void DijkstraDistanceCalculation()
    {
        StartCoroutine(DijkstraRecursiveDistanceCo(vertexA, 0));
    }

    protected IEnumerator DijkstraFindPathCo()
    {
        while (!numerationCompleted) yield return null;
        Vertex<CCell> cur = vertexB;
        List<Vertex<CCell>> neighbors = new List<Vertex<CCell>>();
        Vertex<CCell> shortest = null;
        while(cur != vertexA)
        {
            neighbors = (List<Vertex<CCell>>)cur.Neighbors();
            shortest = neighbors[0];
            foreach (Vertex<CCell> n in neighbors)
                if (n.GetItem().distance < shortest.GetItem().distance)
                    shortest = n;
            DrawEdge(cur.GetItem().transform.position, shortest.GetItem().transform.position, 1);
            cur = shortest;
            yield return animationWait;
        }
    }

    protected IEnumerator DijkstraRecursiveDistanceCo(Vertex<CCell> v, int d)
    {
        yield return animationWait;
        if (!v.GetItem().visited)
        {
            v.GetItem().Visit(d);
            foreach (Vertex<CCell> n in v.Neighbors())
            {
                StartCoroutine(DijkstraRecursiveDistanceCo(n, v.GetItem().distance + 1));
            }
        }
    }

    /// <summary>
    /// Draws and line between given coordinates.
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <param name="destinyList">Used to select to which list of game objects
    /// the new edge is added.</param>
    protected void DrawEdge(Vector3 pos1, Vector3 pos2, int destinyList)
    {
        CSimpleLine newEdge = Instantiate(edgePrefab, transform.position, edgePrefab.transform.rotation).
            GetComponent<CSimpleLine>();
        newEdge.SetPositions(pos1, pos2);
        switch (destinyList)
        {
            case 0:
                edgeGameO.Add(newEdge.gameObject);
                break;
            case 1:
                pathEdgesGameO.Add(newEdge.gameObject);
                break;
            default:
                break;
        }
    }

    protected void NewMazePreparations()
    {
        // maybe change destroying for enabling a disabling individual walls
        for (int i = 0; i < graphGameO.Count; i++)
        {
            Destroy(graphGameO[i].gameObject);
            Destroy(mazeGameO[i].gameObject);
        }
        foreach (GameObject g in edgeGameO) Destroy(g);
        foreach (GameObject g in pathEdgesGameO) Destroy(g);
        graphGameO.Clear();
        mazeGameO.Clear();
        edgeGameO.Clear();
        pathEdgesGameO.Clear();
        graph = new Graph<CCell>();
        if (cellOutlineA != null)
            cellOutlineA.SetActive(false);
        if(cellOutlineB != null)
            cellOutlineB.SetActive(false);
        animationWait = new WaitForSeconds(animationSpeed);
    }
    #endregion
}
