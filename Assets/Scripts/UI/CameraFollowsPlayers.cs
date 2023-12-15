using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayers : MonoBehaviour
{

    public static CameraFollowsPlayers Instance;

    // private float LimitXLeft = 0f;
    // private float LimitXRight = 100-8.5f;
    private Player player1;
    private Player player2;
    private bool isFollowing = true; // Add a flag to control following
    private Vector3 maxDistanceToLeftPlayer = new Vector3(-3.0f, 0, 0);

    private Player leftPlayer;
    public float maxX = Mathf.Infinity;
    // private bool firstScreen = true;
    // private bool bossAlive = true;
    // private GameObject[] enemiesFirstScreen;
    // private GameObject[] boss;
    // private float remainingEnemies;
    // private float remainingBoss;
    void Start()
    {
        player1 = GameManager.Instance.GetPlayer1Script();
        player2 = GameManager.Instance.GetPlayer2Script();
    }

    // Update is called once per frame
    void Update()
    {
        leftPlayer = GameManager.Instance.GetLeftPlayer();
        // Vector3 middlePoint = GameManager.Instance.GetAveragePlayerPosition();
        if (isFollowing && leftPlayer.transform.position.x > maxDistanceToLeftPlayer.x + transform.position.x)
        {
            Vector3 cameraPosition = new Vector3(leftPlayer.transform.position.x - maxDistanceToLeftPlayer.x, transform.position.y, -10);
            if (cameraPosition.x >= maxX)
            {
                cameraPosition.x = maxX;
            }
            transform.position = cameraPosition;
        }

    }

    public void setIsFollowing(bool isFollowing)
    {
        this.isFollowing = isFollowing;
    }

    public void setPosition(float x)
    {
        transform.position = new Vector3(x, 0, -10);
    }

    public void MakeTransition(){
        GameManager.Instance.DisableInput();
        if (
            leftPlayer.transform.position.x <= maxDistanceToLeftPlayer.x + transform.position.x
        )
        {
            setIsFollowing(true);
            GameManager.Instance.EnableInput();
            return;
        }
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * 2;
            if (time > 1)
            {
                time = 1;
            }
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, leftPlayer.transform.position.x - maxDistanceToLeftPlayer.x, time), 0, -10);
            yield return null;
        }
        setIsFollowing(true);
        GameManager.Instance.EnableInput();
    }
}
