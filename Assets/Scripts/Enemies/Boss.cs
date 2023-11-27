using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Boss : MonoBehaviour, CanTakeDamage
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] public int lifeBoss = 1000;
    private Animator animator;
    private Rigidbody2D rb;
    // private bool isMovingUp = true;
    private static readonly int _animationIdle = Animator.StringToHash("Idle");
    private static readonly int _animationAttack = Animator.StringToHash("Attack");
    private static readonly int _animationDefeat = Animator.StringToHash("Defeat");
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    IEnumerator EnemyBehaviour()
    {
        while (true)
        {
            // yield return Move();
            yield return Attack();

            yield return new WaitForSeconds(2.0f);

        }
    }

    void toogleBool(bool b){
        b = !b;
    }

    // IEnumerator Move(){
        // TODO: Move the boss
    // }


    IEnumerator Attack(){
        SetAnimationState(_animationAttack);
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.4f);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        SetAnimationState(_animationIdle);
    }



    public void TakeDamage(int damage) {
        lifeBoss -= damage;
        if (lifeBoss <= 0) {
            SetAnimationState(_animationDefeat);
            Destroy(gameObject, 2.3f);
        }
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        StartCoroutine(ResetColor());
    }
    
    public IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    void SetAnimationState(int state)
    {
        animator.CrossFade(state, 0, 0);
    }

    void OnBecameInvisible()
    {
        StopAllCoroutines();
    }

    void OnBecameVisible()
    {
        StartCoroutine(EnemyBehaviour());
    }
}
