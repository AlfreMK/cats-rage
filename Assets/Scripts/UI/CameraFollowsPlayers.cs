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
        Vector3 middlePoint = GameManager.Instance.GetAveragePlayerPosition();

        if (player1.getIsMounted())
        {
            middlePoint = player2.transform.position;
        }
        if (player2.getIsMounted())
        {
            middlePoint = player1.transform.position;
        }

        if (isFollowing)
        {
            transform.position = new Vector3(Mathf.Clamp(middlePoint.x, LimitXLeft, LimitXRight), 0, -10);
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
