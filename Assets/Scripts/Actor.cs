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
    protected float attackCooldown;
    protected float attackCDTimer;
    protected bool isAlive;

    protected Rigidbody rb;
    protected Animator animator;
    protected ParticleSystem effects;

    protected void Move()
    {
        // calculate movement here
        if (moveVector.magnitude > 1.0f)
        {
            moveVector.Normalize();
        }

        transform.LookAt(transform.position + moveVector);
        animator.SetFloat("Forward", moveVector.magnitude);
        rb.MovePosition(transform.position + (speed * moveVector));
    }

    protected void Attack()
    {
        if (attackCDTimer <= 0)
        {
            //effects.Play();
            animator.SetTrigger("Attack");
            attackCDTimer = attackCooldown;
        }
    }

    public void DealDamage()
    {
        // attack method activates a collider and checks if anything is in it, then passes a damage call if so
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + (transform.forward * attackRange), attackRange);
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

    public virtual void Damage(int damage)
    {
        Debug.Log(gameObject + " was hit for " + damage);
        health -= damage;
        effects.Play();
        animator.SetTrigger("Damaged");
        if (health <= 0)
        {
            animator.SetTrigger("Dead");
            isAlive = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (transform.forward * attackRange), attackRange);
    }
}
