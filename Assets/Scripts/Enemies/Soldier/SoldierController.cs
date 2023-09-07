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
    private Transform player;


    private static readonly int _animationIdle = Animator.StringToHash("Idle");
    private static readonly int _animationAttacking = Animator.StringToHash("Attacking");
    private static readonly int _animationRunning = Animator.StringToHash("Running");


    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("sol");
        player = GameManager.Instance.GetPlayer1();
        StartCoroutine(EnemyBehaviour());
    }


    void SetAnimationState(int state)
    {
        animator.CrossFade(state, 0, 0);
    }

    IEnumerator EnemyBehaviour()
    {
        while (true)
        {

            yield return Move();

            yield return Attack();

            yield return new WaitForSeconds(shotInterval);
        }
    }

    IEnumerator Move()
    {
        SetAnimationState(_animationRunning);

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
        SetAnimationState(_animationAttacking);

        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);

        SetAnimationState(_animationIdle);

    }

    void SpawnGrenade()
    // Esta función se llama en la animación
    {
    
        if (this.player != null)
        {
            Vector3 spawnPosition = transform.position + transform.up * 0.5f;
    
            grenadePrefab.GetComponent<Grenade>().Spawn(spawnPosition, grenadePrefab, this.player.position);
        }

    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0){
            Destroy(gameObject);
        }
    }
}
