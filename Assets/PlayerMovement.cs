using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public int state = 1;
    public Vector3 position;

    public float strafeSpeed;
    public float runSpeed;
    public Transform[] targets;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.position = targets[1].position;
    }

    void Update()
    {
        position = targets[state].position;
    }
    
    void FixedUpdate()
    {
        rb.velocity = (position - rb.position) * strafeSpeed;
        GameObject.Find("Particle System").transform.position = this.transform.position;
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
        Debug.Log(collision.gameObject.tag);
        if (collision.collider.gameObject.CompareTag("deg"))
        {
            GameObject.Find("Particle System").GetComponent<ParticleSystem>().Play();
            Destroy(this.gameObject);
        }
        else if (collision.collider.gameObject.CompareTag("Player"))
        {
            state = Math.Max(0, state - 1);        
        }
            
        
    }
}
