using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemyBar : MonoBehaviour
{
    public GameObject boss;
    private float originalSize;
    private float originalHealth;

    void Start()
    {
        originalHealth = boss.GetComponent<Boss>().lifeBoss;
        originalSize = transform.localScale.x;
    }

    void Update()
    {
        transform.localScale = new Vector3(
            boss.GetComponent<Boss>().lifeBoss / originalHealth * originalSize,
            transform.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(
            (transform.localScale.x - originalSize) / 2, transform.localPosition.y, transform.localPosition.z);
    }
}