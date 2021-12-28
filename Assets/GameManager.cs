using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool started;

    void OnTap()
    {
        if (!started)
        {
            started = true;
            GameObject.Find("Text").SetActive(false); 
        }
        
    }
}
