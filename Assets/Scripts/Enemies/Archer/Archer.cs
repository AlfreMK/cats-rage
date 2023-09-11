using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour, CanTakeDamage
{
    public GameObject arrowPrefab;
    public Transform firePoint;
    public float moveSpeed = 2.0f;
    public float shotInterval = 2.0f;

    private Animator animator;

    // rigidbody
    private Rigidbody2D rb;

    [SerializeField] public int health = 100;
    private static readonly int _animationIdle = Animator.StringToHash("idle");
    private static readonly int _animationAttack = Animator.StringToHash("Attack");
    private static readonly int _animationRun = Animator.StringToHash("Run");

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    IEnumerator EnemyBehaviour()
    {
        while (true)
        {
            yield return Move();
            yield return Attack();

            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator Move(){
        SetAnimationState(_animationRun);

        float xRandomDistance = Random.Range(-1.5f, 1.5f);
        float yRandomDistance = Random.Range(-1.5f, 1.5f);

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + new Vector3(xRandomDistance, yRandomDistance, 0);

        float journeyLength = Vector3.Distance(initialPosition, targetPosition);
        float startTime = Time.time;
        float currJourneyLength = 0.0f;
        while (currJourneyLength < journeyLength)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            currJourneyLength += distanceCovered/journeyLength;
            rb.MovePosition(Vector3.Lerp(initialPosition, targetPosition, fractionOfJourney));
            yield return null;
        }

        SetAnimationState(_animationIdle);
    }

    IEnumerator Attack()
    {
        SetAnimationState(_animationAttack);

        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f);
        SpawnArrow();

        SetAnimationState(_animationIdle);

    }

    void SpawnArrow()
    {
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }

    void SetAnimationState(int state)
    {
        animator.CrossFade(state, 0, 0);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0){
            Destroy(gameObject);
        }
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
