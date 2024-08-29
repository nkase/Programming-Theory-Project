using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    protected float speed;
    protected Vector3 moveVector;

    protected int health;
    protected float attackRange;
    protected int attackDamage;

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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + (Vector3.forward * attackRange), attackRange - 1);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponentInParent<Actor>() != null)
            {
                if (hitCollider.gameObject != gameObject)
                {
                    hitCollider.GetComponentInParent<Actor>().Damage(attackDamage);
                }
            }
        }
    }

    public void Damage(int damage)
    {
        Debug.Log(gameObject + " was hit for " + damage);
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
