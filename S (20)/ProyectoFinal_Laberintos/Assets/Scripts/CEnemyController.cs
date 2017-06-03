using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyController : MonoBehaviour
{
    protected CCell myCell;

    public void SetCell(CCell c)
    {
        myCell = c;
        transform.position = myCell.transform.position;
        CMazeManager.instance.SetVertexB(myCell);
    }
}
