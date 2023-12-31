using System.Collections;
using UnityEngine;

public class SoldierController : MonoBehaviour, CanTakeDamage
{
    public GameObject grenadePrefab;
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 2.0f;
    public float shotInterval = 2.0f;

    [SerializeField] public int health = 100;

    private Animator animator;
    private Transform player1;
    private Transform player2;


    private static readonly int _animationIdle = Animator.StringToHash("Idle");
    private static readonly int _animationAttacking = Animator.StringToHash("Attacking");
    private static readonly int _animationRunning = Animator.StringToHash("Running");


    void Start()
    {
        animator = GetComponent<Animator>();
        player1 = GameManager.Instance.GetPlayer1();
        player2 = GameManager.Instance.GetPlayer2();
    }


    void SetAnimationState(int state)
    {
        animator.CrossFade(state, 0, 0);
    }

    IEnumerator EnemyBehaviour()
    {
        while (true)
        {

            // yield return Move();

            yield return Attack();

            float randomWaitTime = Random.Range(0.5f, 2.0f);

            yield return new WaitForSeconds(randomWaitTime);
        }
    }

    IEnumerator Move()
    {
        SetAnimationState(_animationRunning);

        float xRandomDistance;
        float yRandomDistance;
        Vector3 playerPosition = GameManager.Instance.GetAveragePlayerPosition();
        xRandomDistance = playerPosition.x > transform.position.x ? 1.5f : -1.5f;
        yRandomDistance = playerPosition.y > transform.position.y ? 1.5f : -1.5f;

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
        RotateTowardsPlayer();
        SetAnimationState(_animationAttacking);

        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);

        SetAnimationState(_animationIdle);

    }

    void SpawnGrenade()
    // Esta función se llama en la animación
    {
        Transform randomPlayer = Random.Range(0, 2) == 0 ? this.player1 : this.player2;
    
        if (randomPlayer != null)
        {
            Vector3 spawnPosition = transform.position + transform.up * 0.5f;

    
            grenadePrefab.GetComponent<Grenade>().Spawn(spawnPosition, randomPlayer.position);
        }

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


    void OnBecameInvisible()
    {
        StopAllCoroutines();
    }

    void OnBecameVisible()
    {
        StartCoroutine(EnemyBehaviour());
    }

    void RotateTowardsPlayer()
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
}
