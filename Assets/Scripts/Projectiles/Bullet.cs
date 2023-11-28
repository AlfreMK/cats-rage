using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        CanTakeDamage damageable = hitInfo.GetComponent<CanTakeDamage>();
        if (damageable != null) {
            damageable.TakeDamage(damageBullet);
        }
        if (hitInfo.GetComponent<BothSides>() == null) {
            Destroy(gameObject);    // Destroy the bullet
        }
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

}
