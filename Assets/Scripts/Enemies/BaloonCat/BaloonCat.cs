using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonCat : MonoBehaviour, CanTakeDamage
{
    public GameObject grenadePrefab;
    public float moveSpeed = 2.0f;
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
    }


    void SetAnimationState(int state)
    {
        animator.CrossFade(state, 0, 0);
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x - 0.001f, transform.position.y, 0);
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
}
