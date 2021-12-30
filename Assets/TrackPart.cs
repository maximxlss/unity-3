using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class TrackPart : MonoBehaviour
{
    public GameObject[] src;
    private float speed;
    private int maxparts;
    private bool spawned;
    private Rigidbody rb;
    private GameManager gm;
    
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        maxparts = gm.maxparts;
        speed = gm.speed;
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
                Debug.Log(x);
                var choice = src[x];
                var newpos = this.transform.position;
                newpos += new Vector3(0, 0, this.GetComponentInChildren<BoxCollider>().bounds.size.z-0.1f);
                var obj = Instantiate(choice, Vector3.zero, Quaternion.identity);
                obj.GetComponent<TrackPart>().src = src;
                var offset1 = this.transform.Find("Floor").GetComponent<BoxCollider>().bounds.size.z / 2f;
                var offset2 = obj.transform.Find("Floor").GetComponent<BoxCollider>().bounds.size.z / 2f;
                obj.transform.position = this.transform.position + Vector3.forward * (offset1 + offset2);
                spawned = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (gm.playing)
        {
            rb.MovePosition(rb.position + new Vector3(0, 0, -speed)*Time.deltaTime);
        }
    }
}