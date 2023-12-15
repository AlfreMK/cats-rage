using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Knight : MonoBehaviour, CanTakeDamage
{
    [SerializeField] public int health = 100;

    public AudioSource audioSource;
    public AudioClip swordSwing;

    public int damageSword = 30;

    public float moveSpeed = 3f;
    public float attackCooldown = 2.0f; // Adjust the cooldown time as needed
    public float moveCooldown = 1.2f; // Adjust the cooldown time as needed
    private float attackTimer;
    private float moveTimer;
    public Transform attackPos;
    public float attackRange;
    public LayerMask WhatIsPlayer;

    private Animator animator;
    private static readonly int _animationIdle = Animator.StringToHash("Idle");
    private static readonly int _animationAttack = Animator.StringToHash("Attack");
    private static readonly int _animationRun = Animator.StringToHash("Run");
    private static readonly int _animationJump = Animator.StringToHash("Jump"); // just in case if it's needed

    private Transform player1;
    private Transform player2;
    private Transform randomPlayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player1 = GameManager.Instance.GetPlayer1();
        player2 = GameManager.Instance.GetPlayer2();
        randomPlayer = Random.Range(0, 2) == 0 ? player1 : player2;

        attackTimer = attackCooldown; // Set initial timer value to trigger the first attack
        moveTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTimer > 0 )
        {
            moveTimer -= Time.deltaTime;
            return;
        }
        MoveToPlayer();
        RotateTowardsPlayer();

        // Update the attack timer
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0 && Vector3.Distance(transform.position, randomPlayer.position) < 1.2f)
        {
            SetAnimationState(_animationAttack);
            Attack();
            attackTimer = attackCooldown; // Reset the timer after an attack
            moveTimer = moveCooldown;
            StartCoroutine(ResetAnimationState(_animationIdle, 1.2f));
        }
    }

    void Attack()
    {
        SetAnimationState(_animationAttack);
        audioSource.PlayOneShot(swordSwing, 1f);

        Collider2D[] playersToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsPlayer);
        for (int i = 0; i < playersToDamage.Length; i++)
        {
            playersToDamage[i].GetComponent<Player>().TakeDamage(damageSword);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0){
            Destroy(gameObject);
        }
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        StartCoroutine(ResetColor());
    }

    public IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    public void RotateTowardsPlayer()
    {
        if (randomPlayer.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void SetAnimationState(int state)
    {
        animator.CrossFade(state, 0, 0);
    }

    void MoveToPlayer()
    {
        Vector3 playerPosition = randomPlayer.position;

        if (Vector3.Distance(transform.position, playerPosition) < 1.2f)
        {
        }
        else
        {
            SetAnimationState(_animationRun);
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator ResetAnimationState(int state, float time)
    {
        yield return new WaitForSeconds(time);
        SetAnimationState(state);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}