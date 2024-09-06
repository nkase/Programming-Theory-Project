using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField]
    private GameObject focalPoint;
    private AudioSource walkSound;

    private float speedRef = 0.08f;
    private float maxHealth = 3;
    private int maxXP = 5;
    private int xp = 0;
    private int level = 1;
    private int gold = 0;
    private float sprintModifier = 1.5f;

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
        attackCooldown = 0.5f;
        isAlive = true;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        effects = GetComponentInChildren<ParticleSystem>();
        walkSound = GetComponent<AudioSource>();

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
            if (moveVector.magnitude > 0)
            {
                walkSound.volume = 1;
            }
            else { walkSound.volume = 0; }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
                animator.SetTrigger("Interact");
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("IsSprinting", true);
                speed = speedRef * sprintModifier;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                animator.SetBool("IsSprinting", false);
                speed = speedRef;
            }
        }

        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    xp++;
        //    if (xp >= maxXP)
        //    {
        //        LevelUp();
        //    }
        //    playerXPReport?.Invoke(xp, maxXP, level);
        //}
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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward, 1);
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
        if (isAlive)
        {
            // Debug.Log(gameObject + " was hit for " + damage);
            health -= damage;
            playerHealthReport?.Invoke(health, maxHealth);
            effects.Play();
            animator.SetTrigger("Damaged");

            if (health <= 0)
            {
                animator.SetTrigger("Dead");
                isAlive = false;
            }
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
        maxHealth++;
        health = maxHealth;
        playerHealthReport?.Invoke(health, maxHealth);
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
