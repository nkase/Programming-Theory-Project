using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.5f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ControlMoveVector();
        Move();
    }

    private void ControlMoveVector()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveVector = new Vector3(x, 0, y);
    }
}
