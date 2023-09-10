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

    [SerializeField] public int health = 100;
    private static readonly int _animationIdle = Animator.StringToHash("idle");
    private static readonly int _animationAttack = Animator.StringToHash("Attack");
    private static readonly int _animationRun = Animator.StringToHash("Run");
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(EnemyBehaviour());
    }


    IEnumerator EnemyBehaviour()
    {
        while (true)
        {
            // yield return Move();
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

        while (transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(initialPosition, targetPosition, fractionOfJourney);
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
}
