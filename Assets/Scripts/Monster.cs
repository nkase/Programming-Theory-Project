using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Monster : Actor
{
    private Player player;
    private float hostilityRadius = 20;
    [SerializeField]
    private Vector3 headingToTarget;
    [SerializeField]
    float range;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.05f;
        health = 3;
        attackDamage = 1;
        attackRange = 1;
        attackCooldown = 1;
        isAlive = true;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        effects = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive)
        {
            attackCDTimer -= Time.deltaTime;
            if (player != null)
            {
                MoveVectorToTarget();
                Move();
                range = headingToTarget.magnitude;
                if (headingToTarget.magnitude < attackRange * 2)
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
}
