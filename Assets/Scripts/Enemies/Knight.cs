using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Knight : MonoBehaviour, CanTakeDamage
{
    [SerializeField] public int health = 100;

    public AudioSource audioSource;
    public AudioClip swordSwing;
    
    private Animator animator;
    private static readonly int _animationIdle = Animator.StringToHash("Idle");
    private static readonly int _animationAttack = Animator.StringToHash("Attack");
    private static readonly int _animationRun = Animator.StringToHash("Run");
    private static readonly int _animationJump = Animator.StringToHash("Jump"); // just in case if it's needed

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsPlayer();
    }

    void Move()
    {
        SetAnimationState(_animationRun);
        
        // TODO: do your stuff here
        // ...

        SetAnimationState(_animationIdle);
    }

    void Attack()
    {
        SetAnimationState(_animationAttack);
        audioSource.PlayOneShot(swordSwing, 1f);

        // TODO: do your stuff here
        // ...

        SetAnimationState(_animationIdle);
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
        if (GameManager.Instance.GetAveragePlayerPosition().x > transform.position.x)
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

}