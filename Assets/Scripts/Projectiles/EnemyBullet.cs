using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    [SerializeField] private int damageBullet = 25;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        Player player = hitInfo.GetComponent<Player>();
        
        if (player != null) {
            player.TakeDamage(damageBullet);
        }
        Destroy(gameObject);    // Destroy the bullet
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

}
