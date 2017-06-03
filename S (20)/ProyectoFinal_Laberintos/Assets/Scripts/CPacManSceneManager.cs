using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPacManSceneManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject pizzaPrefab;

    private void Start()
    {
        StartCoroutine(SetupGame());
    }

    private IEnumerator SetupGame()
    {
        CMazeManager.instance.SetMazeSize("15");
        CMazeManager.instance.NewLaberinth();
        Camera.main.transform.position =
            new Vector3(CMazeManager.instance.mazePos.position.x, CMazeManager.instance.mazePos.position.y, -10f);
        while (CMazeManager.instance.processRunning) yield return null;
        CCell playerStartCell = CMazeManager.instance.GetCell(0, 0);
        CCell enemyStartCell = CMazeManager.instance.GetCell(14, 14);
        Instantiate(playerPrefab, playerStartCell.transform.position, playerPrefab.transform.rotation).
            GetComponent<CPlayerController>().myCell = playerStartCell;
        Instantiate(enemyPrefab, enemyStartCell.transform.position, enemyPrefab.transform.rotation).
            GetComponent<CEnemyController>().SetCell(enemyStartCell);
        CMazeManager.instance.animationSpeed = 0f;
        CMazeManager.instance.SetVertexA(playerStartCell);
    }
}
