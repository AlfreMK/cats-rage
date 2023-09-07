using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] private int playerNumber = 1;
    private string shootKey;
    private string punchKey;
    private int healCooldown = 0;
    private int healCooldownMax = 150;

    void Awake() {
        if (playerNumber == 1) {
            shootKey = "Shoot";
            punchKey = "Punch";
        } else if (playerNumber == 2) {
            shootKey = "Shoot2";
            punchKey = "Punch2";
        }
    }

    void Update() {
        if (Input.GetButtonDown(shootKey)) {
            Shoot();
        }
        if (playerNumber == 1 && Input.GetButtonDown(punchKey)) {
            Meele();
        }
        else if (playerNumber == 2 && Input.GetAxisRaw(punchKey) != 0) {
            CoolDownFunction(Heal);
        }
    }

    void Shoot() {
        // Debug.Log("Shooting");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void Heal() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players) {
            if (player.GetComponent<Player>() != null) {
                player.GetComponent<Player>().TakeDamage(-10);
            }
        }
    }

    void Meele() {
        float meleeRadius = 0.5f;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(firePoint.position, meleeRadius);
        for(var hit_index = 0; hit_index < hitColliders.Length; hit_index++)
        {
            if (hitColliders[hit_index].gameObject.GetComponent<CanTakeDamage>() != null) {
                hitColliders[hit_index].gameObject.GetComponent<CanTakeDamage>().TakeDamage(40);
            }

        }
    }

    void CoolDownFunction(System.Action function) {
        if (healCooldown < 0) {
            healCooldown = healCooldownMax;
            function();
        }
        healCooldown--;
    }
}
