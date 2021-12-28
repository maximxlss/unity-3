using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float strafeSpeed = 500f;
    public float runSpeed = 200f;
    private float speedFrontward = 1000f;

    protected bool strafeLeft = false;
    protected bool strafeRight = false;
    protected bool Strafe = false;

    void Update()
    {
        if (Input.GetKey("d"))
        {
            strafeRight = true;
        }
        else
        {
            strafeRight = false;
        }
        if (Input.GetKey("a"))
        {
            strafeLeft = true;
        }
        else
        {
            strafeLeft = false;
        }
        if (Input.GetKey("space"))
        {
            Strafe = true;
        }
        else
        {
            Strafe = false;
        }
    }
    void FixedUpdate()
    {
        rb.AddForce(0, 0, runSpeed * Time.deltaTime);

        if (strafeLeft)
        {
            rb.AddForce(-strafeSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (strafeRight)
        {
            rb.AddForce(strafeSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (Strafe)
        {
            rb.AddForce(0, 0, speedFrontward * Time.deltaTime);
        }
        if (transform.position.x >= 2.962)
        {
            strafeRight = false;
        }
    }

}
