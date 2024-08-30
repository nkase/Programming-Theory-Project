using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField]
    private GameObject focalPoint;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.2f;
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
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

        moveVector = (focalPoint.transform.right * x) + (focalPoint.transform.forward * y);
    }

    private void Interact()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + (Vector3.forward * attackRange), attackRange - 1);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponentInParent<Interactable>() != null)
            {
                hitCollider.GetComponentInParent<Interactable>().Interact(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            if (contact.otherCollider.GetComponentInParent<Block>() != null)
            {
                // cut speed in half while pushing a block
                speed = 0.1f;
            }
        }
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        speed = 0.2f;
    }
}
