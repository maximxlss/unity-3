using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxparts;
    public float speed;
    [NonSerialized] public bool playing;

    void OnTap()
    {
        if (!playing)
        {
            playing = true;
            GameObject.Find("Text").SetActive(false); 
        }
        
    }
}
