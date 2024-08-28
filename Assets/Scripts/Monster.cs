using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Monster : Actor
{
    private Player player;
    private float hostilityRadius = 20;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.3f;
        health = 3;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            MoveVectorToTarget();
            Move();
        }
        else
        {
            FindTarget();
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
        Vector3 headingToTarget = player.transform.position - transform.position;
        if (Vector3.Magnitude(headingToTarget) > hostilityRadius)
        {
            player = null;
            moveVector = Vector3.zero;
        }
        else if (Vector3.Magnitude(headingToTarget) > 3)
        {
            moveVector = headingToTarget;
        }
        else
        {
            moveVector = Vector3.zero;
        }
    }
}
