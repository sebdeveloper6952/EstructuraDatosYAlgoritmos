using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CVertex : MonoBehaviour
{
    protected Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    public void Init(string s) { text.text = s; }
}
