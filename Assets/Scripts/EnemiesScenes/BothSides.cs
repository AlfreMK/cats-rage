using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothSides : MonoBehaviour
{
    public int enemiesToSpawn = 4;
    public int spawnRateInMs = 1000;
    public GameObject enemyType = null;
    public GameObject enemyFlyingType = null;
    public GameObject wall;
    public bool enemiesComingFromBothSides = true;
    public float yMaxGroundHeight = -2.25f;

    private int enemiesSpawned = 0;
    private int enemiesSpawning = 0;
    private int enemiesAlive = 0;
    private GameObject leftWall;
    private GameObject rightWall;
    private BoxCollider2D boxCollider;
    private bool hasTriggered = false;
    private bool hasSpawned = false;

    private float initialPosX;

    public GameObject Arr;
    public GameObject Portal;

    // Start is called before the first frame update
    void Start()
    {   
        boxCollider = GetComponent<BoxCollider2D>();
        initialPosX = transform.position.x;
        // make the wall invisible
        GetComponent<SpriteRenderer>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (hasTriggered) {
            enemiesAlive = GameObject.FindGameObjectsWithTag("EnemyBothSides").Length;
            if (enemiesSpawned == enemiesToSpawn && enemiesAlive == 0 && enemiesSpawning == 0)
            {
                GameManager.Instance.SetMaxX(Mathf.Infinity);
                Destroy(leftWall);
                Destroy(rightWall);
                GameManager.Instance.GetMainCamera().MakeTransition();
                Arr.SetActive(true);
                Destroy(gameObject);
            }
            if (GameManager.Instance.IsCameraInMaxX() && !hasSpawned){
                GameManager.Instance.GetMainCamera().setIsFollowing(false);
                Arr.SetActive(false);
                CreateWalls();
                StartCoroutine(SpawnEnemies());
                hasSpawned = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        if (hitInfo.tag != "Player1") {
            return;
        }
        boxCollider.enabled = false;
        GameManager.Instance.SetMaxX(initialPosX);
        transform.localScale = new Vector3(0.1f, transform.localScale.y, transform.localScale.z);
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
                SpawnRandomEnemy(leftWall.transform.position.x + 0.5f, 0);
            }
            else
            {
                SpawnRandomEnemy(rightWall.transform.position.x - 0.5f, 180);
            }
            enemiesSpawned++;
            enemiesSpawning++;
        }
    }


    void SpawnRandomEnemy(float x, float rotation)
    {
        GameObject enemyPrefab = null;
        float groundYPosition = Random.Range(-4.5f, yMaxGroundHeight);
        float flyingYPosition = Random.Range(0.0f, 0.6f);
        float yPosition;

        if (enemyFlyingType == null) {
            enemyPrefab = enemyType;
            yPosition = groundYPosition;
        } else if (enemyType == null) {
            enemyPrefab = enemyFlyingType;
            yPosition = flyingYPosition;
        } else {
            bool flyingEnemy = Random.Range(0, 2) == 0;
            enemyPrefab = flyingEnemy ? enemyFlyingType : enemyType;
            yPosition = flyingEnemy ? flyingYPosition : groundYPosition;
        }
        GameObject po = Instantiate(Portal, new Vector2(x, yPosition + 0.5f), Quaternion.Euler(new Vector3(0, rotation, 0)));
        float offset = 1.0f;
        if (rotation == 180) {
            offset = -1.0f;
        }
        StartCoroutine(spawnWithDelay(x + offset, yPosition, enemyPrefab));
    }

    IEnumerator spawnWithDelay(float x, float yPosition, GameObject enemyPrefab)
    {
        yield return new WaitForSeconds(1);
        GameObject enemy = Instantiate(enemyPrefab, new Vector2(x, yPosition), Quaternion.identity);
        enemy.tag = "EnemyBothSides";
        yield return new WaitForSeconds(0.1f);
        enemiesSpawning--;
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
