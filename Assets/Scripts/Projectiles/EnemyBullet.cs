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
            GlobalVariables.Instance.AddScore(-damageBullet);
            // Destroy(gameObject); // if we want to destroy the bullet only when it hits a player
        }
        Destroy(gameObject);    // if we want to destroy the bullet when it hits anything
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

}
