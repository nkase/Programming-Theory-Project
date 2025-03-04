using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ABSTRACTION
// Literally just a collection of data structures and behaviours that all actors will share
// and to act as a tag when looking for either players or enemies.
public abstract class Actor : MonoBehaviour
{
    protected float speed;
    protected Vector3 moveVector;

    protected float health;
    public float attackRange;
    public float attackDamage;
    protected float attackCooldown;
    protected float attackCDTimer;
    protected bool isAlive;

    protected Rigidbody rb;
    protected Animator animator;
    protected ParticleSystem effects;
    // Audio layout:
    // 0. Walk Sound
    // 1. Attack Sound
    // 2. Hurt Sound
    protected AudioSource[] soundEffects;
    [SerializeField] protected GameObject attackProjectile;

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
            soundEffects[1].Play();
            animator.SetTrigger("Attack");
            attackCDTimer = attackCooldown;
        }
    }

    //public void DealDamage()
    //{
    //    // attack method activates a collider and checks if anything is in it, then passes a damage call if so
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position + (transform.forward * attackRange), attackRange);
    //    foreach (var hitCollider in hitColliders)
    //    {
    //        if (hitCollider.GetComponentInParent<Actor>() != null)
    //        {
    //            if (hitCollider.gameObject != gameObject)
    //            {
    //                hitCollider.GetComponentInParent<Actor>().Damage(attackDamage);
    //            }
    //        }
    //    }
    //}

    public void DealDamage()
    {

        attackProjectile.transform.position = transform.position + transform.forward;
        attackProjectile.transform.rotation = transform.rotation;
        attackProjectile.SetActive(true);
    }

    public virtual void Damage(float damage)
    {
        Debug.Log(gameObject + " was hit for " + damage);
        health -= damage;
        effects.Play();
        animator.SetTrigger("Damaged");
        soundEffects[2].Play();
        if (health <= 0)
        {
            animator.SetTrigger("Dead");
            isAlive = false;
        }
    }

    public void PlayWalkSound()
    {
        soundEffects[0].Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (transform.forward), 1);
    }
}
