using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public float speed;
    public int maxparts;
    private bool spawned;
    private Rigidbody rb;
    public GameObject[] src;
    private GameManager gm;
    void Start()
    {
        maxparts = 15;
        rb = this.GetComponent<Rigidbody>();
        gm = GameObject.Find("Main Camera").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned && src.Length != 0)
        {
            var gos = GameObject.FindGameObjectsWithTag("FloorPart");
            if (gos.Length < maxparts)
            {
                var x = Random.Range(0, src.Length);
                if (x == src.Length)
                {
                    x -= 1;
                }
                var choice = src[(int)Math.Floor((double)x)];
                var newpos = this.transform.position;
                newpos += new Vector3(0, 0, this.GetComponentInChildren<BoxCollider>().bounds.size.z-0.1f);
                var notold = Instantiate(choice, newpos, Quaternion.identity);
                notold.GetComponent<Spawner>().src = src;
                spawned = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (src.Length != 0 && gm.started)
        {
            rb.MovePosition(rb.position + new Vector3(0, 0, -speed)*Time.deltaTime);
        }
    }
}