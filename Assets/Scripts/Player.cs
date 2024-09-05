using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField]
    private GameObject focalPoint;

    private float speedRef = 0.1f;
    private float maxHealth = 3;
    private int maxXP = 10;
    private int xp = 0;
    private int level = 1;
    private int gold = 0;

    public delegate void PlayerHealthReport(float health, float maxHealth);
    public static event PlayerHealthReport playerHealthReport;

    public delegate void PlayerXPReport(int xp, int maxXP, int level);
    public static event PlayerXPReport playerXPReport;

    public delegate void PlayerGoldReport(int gold);
    public static event PlayerGoldReport playerGoldReport;

    // Start is called before the first frame update
    void Start()
    {
        speed = speedRef;
        health = 3;
        attackRange = 0.1f;
        attackDamage = 1;
        attackCooldown = 1;
        isAlive = true;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        effects = GetComponentInChildren<ParticleSystem>();

        playerHealthReport?.Invoke(health, maxHealth);
        playerXPReport?.Invoke(xp, maxXP, level);
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            xp++;
            if (xp >= maxXP)
            {
                LevelUp();
            }
            playerXPReport?.Invoke(xp, maxXP, level);
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

    public override void Damage(float damage)
    {
        Debug.Log(gameObject + " was hit for " + damage);
        health -= damage;
        playerHealthReport?.Invoke(health,maxHealth);
        effects.Play();
        animator.SetTrigger("Damaged");
        if (health <= 0)
        {
            animator.SetTrigger("Dead");
            isAlive = false;
        }
    }

    public void GainLoot (int lootedGold, int lootedXP)
    {
        gold += lootedGold;
        playerGoldReport?.Invoke(gold);

        xp += lootedXP;
        if (xp >= maxXP)
        {
            LevelUp();
        }
        playerXPReport?.Invoke(xp, maxXP, level);
    }

    public void LevelUp()
    {
        xp = xp - maxXP;
        level++;
        attackDamage = 0.5f + (0.5f * level);
        speedRef = 0.1f + (0.01f * level);
    }

    private void OnCollisionEnter()
    {
        // cut speed in half while pushing a block
        // speed = speedRef / 2;
    }

    private void OnCollisionExit()
    {
        // reset speed on collision exit
        speed = speedRef;
    }
}
