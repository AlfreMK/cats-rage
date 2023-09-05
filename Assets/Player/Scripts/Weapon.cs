using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] private int playerNumber = 1;
    private string shootKey;

    // Update is called once per frame
    void Awake() {
        if (playerNumber == 1) {
            shootKey = "Shoot";
        } else if (playerNumber == 2) {
            shootKey = "Shoot2";
        }
    }

    void Update() {
        if (Input.GetButtonDown(shootKey)) {
            Shoot();
        }
    }

    void Shoot() {
        // Shooting logic
        // Debug.Log("Shooting");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
