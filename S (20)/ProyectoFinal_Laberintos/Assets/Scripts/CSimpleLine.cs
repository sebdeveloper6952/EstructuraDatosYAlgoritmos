using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(LineRenderer))]
public class CSimpleLine : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 pos2;

    private LineRenderer line;

    public void SetPositions(Vector3 pos1, Vector3 pos2)
    {
        this.pos1 = pos1;
        this.pos2 = pos2;
        Vector3[] a = { pos1, pos2};
        line.SetPositions(a);
    }

    public void SetPositions()
    {
        SetPositions(pos1, pos2);
    }

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
}
