using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour, CanTakeDamage
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float moveSpeed = 2.0f;
    public float shotInterval = 2.0f;
    [SerializeField] public int health = 100;
    public AudioSource audioSource;
    public AudioClip clipProjectile;

    private Animator animator;

    private int _animationIdle;
    private int _animationAttack;
    private int _animationRun;
    private Player player1;
    private Player player2;

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AsignAnimation(string idle, string attack, string run)
    {
        _animationIdle = Animator.StringToHash(idle);
        _animationAttack = Animator.StringToHash(attack);
        _animationRun = Animator.StringToHash(run);
    }

    public IEnumerator EnemyBehaviour()
    {
        while (true)
        {
            yield return Move();
            yield return Attack();

            yield return new WaitForSeconds(1.0f);
        }
    }

    public IEnumerator Move(){
        SetAnimationState(_animationRun);

        float xRandomDistance;
        float yRandomDistance;
        Vector3 playerPosition = GameManager.Instance.GetAveragePlayerPosition();
        xRandomDistance = playerPosition.x > transform.position.x ? 1.5f : -1.5f;
        yRandomDistance = playerPosition.y > transform.position.y ? 1.5f : -1.5f;

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
            transform.position = Vector3.Lerp(initialPosition, targetPosition, fractionOfJourney);

            yield return null;
        }

        SetAnimationState(_animationIdle);
    }

    public IEnumerator Attack()
    {
        // invert the direction of the sprite if the player is on the right
        RotateTowardsPlayer();
        audioSource.PlayOneShot(clipProjectile, 1f);
        SetAnimationState(_animationAttack);

        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f);
        SpawnProyectile();

        SetAnimationState(_animationIdle);

    }

    public void SpawnProyectile()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    public void SetAnimationState(int state)
    {
        animator.CrossFade(state, 0, 0);
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

    public void OnBecameInvisible()
    {
        StopAllCoroutines();
    }

    public void OnBecameVisible()
    {
        StartCoroutine(EnemyBehaviour());
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
}
