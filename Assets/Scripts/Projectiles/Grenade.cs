using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float desiredTimeOfFlight = 2.0f;

    public Rigidbody2D rb;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
    }


    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    public void Spawn(Vector3 spawnPosition, GameObject grenadePrefabRef, Vector3 playerPosition){
        
        GameObject grenade = Instantiate(gameObject, spawnPosition, Quaternion.identity);
        Vector2 toPlayer = playerPosition - grenade.transform.position;

        float horizontalSpeed = toPlayer.x / desiredTimeOfFlight;

        float verticalSpeed = toPlayer.y / desiredTimeOfFlight + 0.5f * 9.81f * desiredTimeOfFlight;

        Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
    }

}
