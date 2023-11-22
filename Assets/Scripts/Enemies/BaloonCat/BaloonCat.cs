using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonCat : MonoBehaviour, CanTakeDamage
{
    public GameObject grenadePrefab;
    public float moveSpeed = 0.001f;
    public float rotationSpeed = 2.0f;
    public float leftShotInterval = 4.0f;
    public float rightShotInterval = 8.0f;

    public int health = 40;

    private Animator animator;
    private Transform player1;
    private Transform player2;


    private static readonly int _animationIdle = Animator.StringToHash("Idle");


    void Start()
    {
        animator = GetComponent<Animator>();
        player1 = GameManager.Instance.GetPlayer1();
        player2 = GameManager.Instance.GetPlayer2();
        RotateTowardsPlayer();
    }


    void SetAnimationState(int state)
    {
        animator.CrossFade(state, 0, 0);
    }

    void Update()
    {
        int direction = transform.rotation.eulerAngles.y == 0 ? -1 : 1;
        transform.position += new Vector3(direction * moveSpeed, 0, 0);
    }

    IEnumerator EnemyBehaviour()
    {
        while (true)
        {
            SpawnGrenade();

            float randomWaitTime = Random.Range(leftShotInterval, rightShotInterval);

            yield return new WaitForSeconds(randomWaitTime);
        }
    }


    void Attack()
    {
        RotateTowardsPlayer();
        SpawnGrenade();
    }

    void SpawnGrenade()
    // Esta función se llama en la animación
    {
        Transform randomPlayer = Random.Range(0, 2) == 0 ? player1 : player2;
        if (randomPlayer != null)
        {
            Vector3 spawnPosition = transform.position;
    
            grenadePrefab.GetComponent<Grenade>().Spawn(spawnPosition, randomPlayer.position);
        }

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
