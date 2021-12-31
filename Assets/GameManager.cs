using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxparts;
    public float speed;
    public float acceleration;
    [NonSerialized] public float time;
    [NonSerialized] public float score;
    [NonSerialized] public bool playing;
    private float startTime;
    private bool end;

    private void Update()
    {
        if (playing)
        {
            // speed += acceleration * Time.deltaTime;
            score += speed * Time.deltaTime;
            time = Time.timeSinceLevelLoad - startTime;
            end = true;
        }
    }

    void OnTap()
    {
        if (!playing && end==false)
        {
            playing = true;
            GameObject.Find("Tap to play").SetActive(false);
            startTime = Time.timeSinceLevelLoad;
        }
    }
}
