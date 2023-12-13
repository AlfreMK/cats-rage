using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayers : MonoBehaviour
{

    public static CameraFollowsPlayers Instance;

    private float LimitXLeft = 0f;
    private float LimitXRight = 100-8.5f;
    private Player player1;
    private Player player2;
    private bool isFollowing = true; // Add a flag to control following
    private Vector3 maxDistanceToLeftPlayer = new Vector3(-3.0f, 0, 0);
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
        // Vector3 middlePoint = GameManager.Instance.GetAveragePlayerPosition();
        Vector3 leftPlayer = player1.transform.position;
        if (player2.transform.position.x < leftPlayer.x)
        {
            leftPlayer = player2.transform.position;
        }

        if (isFollowing && leftPlayer.x > maxDistanceToLeftPlayer.x + transform.position.x)
        {
            Vector3 cameraPosition = new Vector3(leftPlayer.x - maxDistanceToLeftPlayer.x, 0, -10);
            if (cameraPosition.x >= maxX)
            {
                cameraPosition.x = maxX;
            }
            transform.position = new Vector3(Mathf.Clamp(cameraPosition.x, LimitXLeft, LimitXRight), 0, -10);
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
}
