using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private GameManager gm;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        this.GetComponent<Text>().text = ((int) gm.score).ToString();
    }
}
