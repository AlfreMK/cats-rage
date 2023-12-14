using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class BossFightOne : MonoBehaviour
{
    public GameObject wall;

    [ SerializeField] public AudioSource mainMusic;

    [SerializeField] public GameObject consistencyWall;
    [SerializeField] public Boss boss;

    private PlayableDirector playableDirector;

    private GameObject leftWall;
    private GameObject rightWall;
    private BoxCollider2D boxCollider;
    private bool hasTriggered = false;
    private bool hasSpawned = false;

    private float initialPosX;

    // Start is called before the first frame update
    void Start()
    {   
        boxCollider = GetComponent<BoxCollider2D>();
        initialPosX = transform.position.x;
        // make the wall invisible
        GetComponent<SpriteRenderer>().enabled = false;
        playableDirector = GetComponent<PlayableDirector>();
    }
    // Update is called once per frame
    void Update()
    {
        if (hasTriggered) {
            if (boss.lifeBoss <= 0)
            {
                GameManager.Instance.SetMaxX(Mathf.Infinity);
                Destroy(leftWall);
                Destroy(rightWall);
                GameManager.Instance.GetMainCamera().MakeTransition();
                Destroy(gameObject, 2.4f);
                mainMusic.Play();
            }
            if (GameManager.Instance.IsCameraInMaxX() && !hasSpawned){
                Player player1 = GameManager.Instance.GetPlayer1Script();
                Player player2 = GameManager.Instance.GetPlayer2Script();
                player1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                player2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GameManager.Instance.GetMainCamera().setIsFollowing(false);
                GameManager.Instance.DisableInput();
                CreateWalls();
                hasSpawned = true;
                StartFightAnimation();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        if (hitInfo.tag != "Player1") {
            return;
        }
        boxCollider.enabled = false;
        GameManager.Instance.SetMaxX(initialPosX);
        hasTriggered = true;
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
        leftWall.transform.localScale = new Vector3(0.1f, transform.localScale.y, transform.localScale.z);
        rightWall.transform.localScale = new Vector3(0.1f, transform.localScale.y, transform.localScale.z);
    }

    void StartFightAnimation()
    {
        mainMusic.Stop();
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        playableDirector.Play();
    }

    public void EndAnimation()
    {
        GameManager.Instance.EnableInput();
        consistencyWall.SetActive(false);
        boss.startAttacking();

    }

}
