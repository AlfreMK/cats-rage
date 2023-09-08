using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Boss : MonoBehaviour, CanTakeDamage
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;
    [SerializeField] public int lifeBoss = 1000;
    [SerializeField] private int attackSpeed = 300;
    private int coolDown = 0;
    private bool isGoingUp = true;
    private bool isDefeated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // limits -4.85f, -1.75f
        // move up and down

        if (lifeBoss <= 0 && !isDefeated) {
            animator.SetTrigger("Defeat");
            isDefeated = true;
            Destroy(gameObject, 2.3f);
        }
        if (!isDefeated) {
            CoolDownFunction(Shoot);
            if (isGoingUp) {
            transform.Translate(Vector2.up * Time.deltaTime * 0.5f);
            if (transform.position.y >= -1.75f) {
                isGoingUp = false;
            }
            } else {
                transform.Translate(Vector2.down * Time.deltaTime * 0.5f);
                if (transform.position.y <= -4.85f) {
                    isGoingUp = true;
                }
            }
        }
        

    }

    void Shoot() {
        // Debug.Log("Shooting");
        animator.SetTrigger("Attack");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void CoolDownFunction(System.Action function) {
        if (coolDown < 0) {
            coolDown = attackSpeed;
            function();
        }
        coolDown--;
    }

    void wait(int frames) {
        int i = 0;
        while (i < frames) {
            i++;
        }
    }

    public void TakeDamage(int damage) {
        lifeBoss -= damage;
    }
}
