using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFirstScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider2D boxCollider;
    private GameObject[] enemiesFirstScreen;
    private GameObject[] boss;
    private int remainingBoss;
    private float remainingEnemies;
    void Start()
    {
        // make the wall invisible
        GetComponent<SpriteRenderer>().enabled = false;
    }
    void Update()
    {
        boss = GameObject.FindGameObjectsWithTag("Boss");
        remainingBoss = boss.Length;

        if (remainingBoss == 0)
        {
            boxCollider.enabled = false;
        }
    }
}