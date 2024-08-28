using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Actor : MonoBehaviour
{
    protected float speed;
    protected Vector3 moveVector;

    protected int health;

    protected Rigidbody rb;

    protected void Move()
    {
        // calculate movement here
        if (moveVector.magnitude > 1.0f)
        {
            moveVector.Normalize();
        }
        rb.MovePosition(transform.position + (speed * moveVector));
    }

    protected void Attack()
    {
        // attack method activates a collider and checks if anything is in it, then passes a damage call if so
    }

    public void Damage(int damage)
    {
        health -= damage;
    }
}
