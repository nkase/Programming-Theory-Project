using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// INHERITANCE
public class Monster : Actor
{
    private Player player;
    private float hostilityRadius = 8;
    private Vector3 headingToTarget;

    // ENCAPSULATION
    public delegate void OnMonsterHostility();
    public static event OnMonsterHostility onMonsterHostility;

    public delegate void OnMonsterPassivity();
    public static event OnMonsterPassivity onMonsterPassivity;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.03f;
        health = 2;
        attackDamage = 1;
        attackRange = 0.1f;
        attackCooldown = 1;
        isAlive = true;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        effects = GetComponentInChildren<ParticleSystem>();
        soundEffects = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    // ABSTRACTION
    void FixedUpdate()
    {
        if (isAlive)
        {
            attackCDTimer -= Time.deltaTime;
            if (player != null)
            {
                MoveVectorToTarget();
                Move();
                if (headingToTarget.magnitude < attackRange*20)
                {
                    Attack();
                }
            }
            else
            {
                FindTarget();
            }
        }
    }

    private void FindTarget()
    {
        // cast out a sphere with hostilityRadius size
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, hostilityRadius);

        // check if any of the colliders in the sphere are the player
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponentInParent<Player>() != null)
            {
                player = hitCollider.GetComponentInParent<Player>();
                onMonsterHostility?.Invoke();
            }
        }
    }

    private void MoveVectorToTarget()
    {
        headingToTarget = player.transform.position - transform.position;
        if (Vector3.Magnitude(headingToTarget) > hostilityRadius * 1.5)
        {
            player = null;
            moveVector = Vector3.zero;
            onMonsterPassivity?.Invoke();
        }
        else if (Vector3.Magnitude(headingToTarget) > 1.5)
        {
            moveVector = headingToTarget;
        }
        else
        {
            moveVector = Vector3.zero;
        }
    }

    private void DropLootOnDeath()
    {
        GameObject loot = ObjectPool.SharedInstance.GetPooledObject();
        if (loot != null)
        {
            loot.transform.position = transform.position;
            loot.transform.rotation = transform.rotation;
            loot.SetActive(true);
        }
    }

    // POLYMORPHISM
    public override void Damage(float damage)
    {
        if (isAlive)
        {
            //Debug.Log(gameObject + " was hit for " + damage);
            health -= damage;
            effects.Play();
            animator.SetTrigger("Damaged");
            soundEffects[2].Play();
            if (health <= 0)
            {
                animator.SetTrigger("Dead");
                DropLootOnDeath();
                onMonsterPassivity?.Invoke();
                isAlive = false;
            }
        }
    }
}
