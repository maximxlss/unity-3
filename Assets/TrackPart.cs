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
    private Vector3 initPos;
    
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
                var choice = src[x];
                var obj = Instantiate(choice, Vector3.zero, Quaternion.identity);
                var objb = obj.GetComponent<TrackPart>();
                objb.src = src;
                var offset1 = this.transform.Find("Floor").GetComponent<BoxCollider>().bounds.size.z / 2f;
                var offset2 = obj.transform.Find("Floor").GetComponent<BoxCollider>().bounds.size.z / 2f;
                objb.initPos = this.initPos + Vector3.forward * (offset1 + offset2);
                obj.transform.position = objb.initPos;
                spawned = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!gm.playing)
            return;
        // TODO: prevent infinite initPos growth
        rb.MovePosition(initPos + Vector3.back * speed * gm.time);
    }
}