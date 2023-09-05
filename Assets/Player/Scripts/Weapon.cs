using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject healPrefab = null;
    [SerializeField] private int playerNumber = 1;
    private string shootKey;
    private string punchKey;

    // Update is called once per frame
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
        if (playerNumber == 2 && Input.GetButtonDown(punchKey)) {
            Heal();
        }
    }

    void Shoot() {
        // Shooting logic
        // Debug.Log("Shooting");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void Heal() {
        Instantiate(healPrefab, firePoint.position, firePoint.rotation);
    }
}
