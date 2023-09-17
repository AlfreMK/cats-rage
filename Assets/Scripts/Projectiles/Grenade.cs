using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float desiredTimeOfFlight = 2.0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SpawnExplosion(this.transform));
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Spawn(Vector3 spawnPosition, Vector3 playerPosition)
    {
        GameObject grenade = Instantiate(gameObject, spawnPosition, Quaternion.identity);
        Vector2 toPlayer = playerPosition - grenade.transform.position;

        float horizontalSpeed = toPlayer.x / desiredTimeOfFlight;
        float verticalSpeed = toPlayer.y / desiredTimeOfFlight + 0.5f * 9.81f * desiredTimeOfFlight;

        rb = grenade.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

    IEnumerator SpawnExplosion(Transform grenadeTransform)
    {
        yield return new WaitForSeconds(desiredTimeOfFlight);
        Instantiate(explosionPrefab, grenadeTransform.position + (explosionPrefab.transform.up), Quaternion.identity);
        float explosionRadius = 1.5f;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(grenadeTransform.position, explosionRadius);
        for(var hit_index = 0; hit_index < hitColliders.Length; hit_index++)
        {
            if (hitColliders[hit_index].gameObject.GetComponent<Player>() != null) {
                hitColliders[hit_index].gameObject.GetComponent<Player>().TakeDamage(40);
            }
            else if (hitColliders[hit_index].gameObject.GetComponent<CanTakeDamage>() != null)
            {
                hitColliders[hit_index].gameObject.GetComponent<CanTakeDamage>().TakeDamage(100);
            }
        }
        Destroy(grenadeTransform.gameObject); 
    }
}
