using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject grenadePrefab = null;
    public AudioSource audioSource;
    public AudioClip clip;


    [SerializeField] private int playerNumber = 1;
    private string punchKey;
    private bool isSpecialCooldown = false;
    public int grenadesQuantity = 3;
    private Player player1;
    private Player player2;


    void Start()
    {
        player1 = GameManager.Instance.GetPlayer1Script();
        player2 = GameManager.Instance.GetPlayer2Script();
        if (playerNumber == 1)
        {
            punchKey = "Punch";
        }
        else if (playerNumber == 2)
        {
            punchKey = "Punch2";
        }
    }

    void Update()
    {
        if (playerNumber == 1 && GameManager.Instance.IsInputEnabled())
        {
            if (Input.GetButtonDown(punchKey))
            {
                if (grenadesQuantity > 0)
                {
                    SpawnGrenade();
                }
                else
                {
                    Meele();
                }
            }
        }
        else if (playerNumber == 2 && GameManager.Instance.IsInputEnabled())
        {
            if (Input.GetAxisRaw(punchKey) != 0 && !isSpecialCooldown)
            {
                StartCoroutine(HealWithCooldown(3.0f));
            }
        }
    }

    IEnumerator HealWithCooldown(float cooldownTime)
    {
        isSpecialCooldown = true;
        Heal();
        yield return new WaitForSeconds(cooldownTime);
        isSpecialCooldown = false;
    }

    void Shoot()
    {
        audioSource.PlayOneShot(clip, 1f);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void Heal()
    {
        player1.TakeDamage(-20);
        player2.TakeDamage(-20);
    }

    void Meele()
    {
        float meleeRadius = 0.5f;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(firePoint.position, meleeRadius);
        for (var hit_index = 0; hit_index < hitColliders.Length; hit_index++)
        {
            if (hitColliders[hit_index].gameObject.GetComponent<CanTakeDamage>() != null)
            {
                hitColliders[hit_index].gameObject.GetComponent<CanTakeDamage>().TakeDamage(40);
            }
        }
    }

    void SpawnGrenade()
    {
        Vector3 spawnPosition = transform.position + transform.up * 0.5f;
        Vector3 targetPosition = transform.position + transform.up * 0.5f + transform.right * 6f;
        grenadePrefab.GetComponent<Grenade>().Spawn(spawnPosition, targetPosition);
        grenadesQuantity--;
    }
}
