using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothSides : MonoBehaviour
{
    public int enemiesToSpawn = 4;
    public int spawnRateInMs = 1000;
    public GameObject enemyType;
    public GameObject enemyFlyingType = null;
    public GameObject wall;
    public bool enemiesComingFromBothSides = true;
    public float yMaxGroundHeight = -2.25f;

    private int enemiesSpawned = 0;
    private int enemiesAlive = 0;
    private GameObject leftWall;
    private GameObject rightWall;
    private BoxCollider2D boxCollider;
    private bool hasTriggered = false;

    // Start is called before the first frame update
    void Start()
    {   
        boxCollider = GetComponent<BoxCollider2D>();
        // make the green square much much thinner
        transform.localScale = new Vector3(0.01f, transform.localScale.y, transform.localScale.z);
        // make the wall invisible
        GetComponent<SpriteRenderer>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (hasTriggered) {
            enemiesAlive = GameObject.FindGameObjectsWithTag("EnemyBothSides").Length;
            if (enemiesSpawned == enemiesToSpawn && enemiesAlive == 0)
            {
                GameManager.Instance.GetMainCamera().setIsFollowing(true);
                Destroy(leftWall);
                Destroy(rightWall);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        // setting camera
        GameManager.Instance.GetMainCamera().setIsFollowing(false);
        GameManager.Instance.GetMainCamera().setPosition(transform.position.x);
        
        CreateWalls();
        StartCoroutine(SpawnEnemies());
        boxCollider.enabled = false;
        hasTriggered = true;
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < enemiesToSpawn)
        {
            yield return new WaitForSeconds(spawnRateInMs / 1000);
            // spawn left, then right and repeat
            if (enemiesSpawned % 2 == 0 && enemiesComingFromBothSides)
            {
                SpawnRandomEnemy(leftWall.transform.position.x + 0.5f);
            }
            else
            {
                SpawnRandomEnemy(rightWall.transform.position.x - 0.5f);
            }
            enemiesSpawned++;
            enemiesAlive++;
        }
    }


    void SpawnRandomEnemy(float x)
    {
        GameObject enemyPrefab = null;
        float groundYPosition = Random.Range(-4f, yMaxGroundHeight);
        float flyingYPosition = Random.Range(0f, 0.8f);
        float yPosition;

        if (enemyFlyingType == null) {
            enemyPrefab = enemyType;
            yPosition = groundYPosition;
        } else {
            bool flyingEnemy = Random.Range(0, 2) == 0;
            enemyPrefab = flyingEnemy ? enemyFlyingType : enemyType;
            yPosition = flyingEnemy ? flyingYPosition : groundYPosition;
        }

        GameObject enemy = Instantiate(enemyPrefab, new Vector2(x, yPosition), Quaternion.identity);
        enemy.tag = "EnemyBothSides";
    }
    
    // get edges of both sides
    Vector2 GetEdges()
    {
        float left = transform.position.x - 8.5f;
        float right = transform.position.x + 8.5f;
        return new Vector2(left, right);
    }


    void CreateWalls()
    {
        Vector2 edges = GetEdges();
        Vector2 leftWallPosition = new Vector2(edges.x, transform.position.y);
        Vector2 rightWallPosition = new Vector2(edges.y, transform.position.y);
        leftWall = Instantiate(wall, leftWallPosition, Quaternion.identity);
        rightWall = Instantiate(wall, rightWallPosition, Quaternion.identity);
        leftWall.transform.localScale = transform.localScale;
        rightWall.transform.localScale = transform.localScale;
    }

}
