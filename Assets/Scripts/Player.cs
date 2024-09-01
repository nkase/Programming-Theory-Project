using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField]
    private GameObject focalPoint;

    private float speedRef = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        speed = speedRef;
        health = 3;
        attackRange = 1;
        attackDamage = 1;
        attackCooldown = 1;
        isAlive = true;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        effects = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            attackCDTimer -= Time.deltaTime;
            ControlMoveVector();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
                animator.SetTrigger("Interact");
            }
        }
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            Move();
        }
    }

    private void ControlMoveVector()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveVector = (focalPoint.transform.right * x) + (focalPoint.transform.forward * y);
    }

    private void Interact()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + (transform.forward * attackRange), attackRange - 1);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponentInParent<Interactable>() != null)
            {
                hitCollider.GetComponentInParent<Interactable>().Interact(gameObject);
            }
        }
    }

    private void OnCollisionEnter()
    {
        // cut speed in half while pushing a block
        //speed = speedRef / 2;
    }

    private void OnCollisionExit()
    {
        // reset speed on collision exit
        speed = speedRef;
    }
}
