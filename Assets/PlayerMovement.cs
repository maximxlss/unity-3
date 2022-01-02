﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public int state = 1;
    public Vector3 position;
    public float strafeSpeed;
    public float runSpeed;
    public Transform[] targets;
    private bool ismovable = true;
    public bool addwatch;
    private int scaling = 2;
    private GameManager gm;


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.position = targets[1].position;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        position = targets[state].position;
    }
    
    void FixedUpdate()
    {
        rb.velocity = (position - rb.position) * strafeSpeed;
        if (ismovable)
        {
            GameObject.Find("Particle System").transform.position = this.transform.position;
        }
    }

    void OnTurnleft()
    {
        state = Math.Max(0, state - 1);
    }
    
    void OnTurnright()
    {
        state = Math.Min(2, state+1);
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider.gameObject;
        if (other.CompareTag("deg"))
        {
            this.GetComponent<MeshRenderer>().enabled=false;
            this.GetComponent<SphereCollider>().enabled=false;
            GameObject.Find("Particle System").GetComponent<ParticleSystem>().Play();
            ismovable = false;
            gm.playing = false;
            gm.gameOver = true;
            Invoke(nameof(Restart), 3f);
        }
        else if (other.CompareTag("portal"))
        {
            
            foreach (var col in other.GetComponentsInChildren<Collider>())
            {
                col.enabled = false;
            }
            transform.localScale = new Vector3(scaling,scaling,scaling);
            var prt = GameObject.FindGameObjectsWithTag("portal");
            scaling += 1;
        }
            
        
    }
    private void Restart()
    {
        Debug.Log("GameOver");
        if (addwatch == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);    
        }
        else
        {
            
        }
            
        
    }
}   
