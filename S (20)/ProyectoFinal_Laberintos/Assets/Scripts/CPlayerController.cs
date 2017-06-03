using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerController : MonoBehaviour
{
    public CCell myCell;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (!myCell.walls[0].activeSelf)
            {
                myCell = CMazeManager.instance.GetCell(myCell.row - 1, myCell.col);
                transform.position = myCell.transform.position;
                CMazeManager.instance.SetVertexA(myCell);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {

            if (!myCell.walls[2].activeSelf)
            {
                myCell = CMazeManager.instance.GetCell(myCell.row + 1, myCell.col);
                transform.position = myCell.transform.position;
                CMazeManager.instance.SetVertexA(myCell);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {

            if (!myCell.walls[3].activeSelf)
            {
                myCell = CMazeManager.instance.GetCell(myCell.row, myCell.col - 1);
                transform.position = myCell.transform.position;
                CMazeManager.instance.SetVertexA(myCell);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {

            if (!myCell.walls[1].activeSelf)
            {
                myCell = CMazeManager.instance.GetCell(myCell.row, myCell.col + 1);
                transform.position = myCell.transform.position;
                CMazeManager.instance.SetVertexA(myCell);
            }
        }
    }
}
