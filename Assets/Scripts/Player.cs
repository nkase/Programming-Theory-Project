using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.3f;
        health = 3;
        attackRange = 5;
        attackDamage = 1;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlMoveVector();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void ControlMoveVector()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveVector = new Vector3(x, 0, y);
    }
}
