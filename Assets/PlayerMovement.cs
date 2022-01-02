using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float strafeSpeed;
    public float partsOffset;
    public Transform[] targets;
    public GameObject partPrefab;
    private Rigidbody rb;
    private int state = 1;
    private Vector3 position;
    private GameManager gm;
    private Stack<GameObject> parts = new Stack<GameObject>();
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.MovePosition(targets[1].position);
        position = targets[state].position;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        parts.Push(Instantiate(partPrefab, this.transform));
    }

    void FixedUpdate()
    {
        rb.velocity = (position - rb.position) * strafeSpeed;
    }

    void OnTurnleft()
    {
        state = Math.Max(0, state - 1);
        position = targets[state].position;
    }
    
    void OnTurnright()
    {
        state = Math.Min(2, state+1);
        position = targets[state].position;
    }

    private void OnCollision(Collision collision)
    {
        if (!gm.playing)
            return;
        var other = collision.collider.gameObject;
        if (other.CompareTag("deg"))
        {
            Destroy(other);
            Destroy(parts.Pop());
            if (parts.Count == 0)
                Die();
        }
        else if (other.CompareTag("portal"))
        {
            foreach (var col in other.GetComponentsInChildren<Collider>())
            {
                col.enabled = false;
            }
            parts.Push(Instantiate(partPrefab, this.transform));
            parts.Peek().transform.localPosition = Vector3.back * partsOffset * (parts.Count - 1);
        }
    }
    
    // !!!
    private void OnCollisionStay(Collision collisionInfo)
    {
        OnCollision(collisionInfo);
    }
    // !!!
    private void OnCollisionEnter(Collision collisionInfo)
    {
        OnCollision(collisionInfo);
    }

    private void Die()
    {
        gm.playing = false;
        Invoke(nameof(Restart), 2f);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}   
