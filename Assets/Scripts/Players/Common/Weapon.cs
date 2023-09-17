using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject grenadePrefab = null;
    [SerializeField] private int playerNumber = 1;
    private string shootKey;
    private string punchKey;
    private int coolDownShoot = 0;
    private int coolDownShootMax = 70;
    private int coolDownSpecial = 0;
    private int coolDownSpecialMax = 300;
    public int grenadesQuantity = 0;
    private Player player1;
    private Player player2;

    void Start() {
        player1 = GameManager.Instance.GetPlayer1Script();
        player2 = GameManager.Instance.GetPlayer2Script();
        if (playerNumber == 1) {
            shootKey = "Shoot";
            punchKey = "Punch";
            grenadesQuantity = 3;
        } else if (playerNumber == 2) {
            shootKey = "Shoot2";
            punchKey = "Punch2";
        }
    }

    void Update() {
        if (Input.GetAxisRaw(shootKey) != 0) {
            CoolDownFunction(Shoot, ref coolDownShoot, coolDownShootMax);
        }
        if (playerNumber == 1 && Input.GetButtonDown(punchKey)) {
            if (grenadesQuantity > 0){
                SpawnGrenade();
            }
            else {
                Meele();
            }
        }
        else if (playerNumber == 2 && Input.GetAxisRaw(punchKey) != 0) {
            CoolDownFunction(Heal, ref coolDownSpecial, coolDownSpecialMax);
        }
    }

    void Shoot() {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void Heal() {
        player1.TakeDamage(-20);
        player2.TakeDamage(-20);
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

    void SpawnGrenade(){
        if (grenadesQuantity <= 0) {
            return;
        }
        Vector3 spawnPosition = transform.position + transform.up * 0.5f;
        Vector3 targetPosition = transform.position + transform.up * 0.5f + transform.right * 6f;
        grenadePrefab.GetComponent<Grenade>().Spawn(spawnPosition, targetPosition);
        grenadesQuantity --;
    }


    void CoolDownFunction(System.Action function, ref int cooldown, int cooldownMax) {
        if (cooldown > cooldownMax) {
            cooldown = 0;
            function();
        }
        cooldown++;
    }
}
