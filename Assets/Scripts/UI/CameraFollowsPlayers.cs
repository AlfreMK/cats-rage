using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowsPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    private float LimitXLeft = 0f;
    private float LimitXRight = 100 - 8.5f;
    private bool isFollowing = true; // Add a flag to control following
    private bool firstScreen = true;
    private bool bossAlive = false;
    private GameObject[] enemiesFirstScreen;
    private GameObject[] boss;
    private float remainingEnemies;
    private float remainingBoss;

    // Add a method to stop following
    public void StopFollowing()
    {
        isFollowing = false;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemiesFirstScreen = GameObject.FindGameObjectsWithTag("EnemiesFirstScreen");
        remainingEnemies = enemiesFirstScreen.Length;

        boss = GameObject.FindGameObjectsWithTag("Boss");
        remainingBoss = boss.Length;

        Vector3 middlePoint = GameManager.Instance.GetAveragePlayerPosition();
        Debug.Log(middlePoint.x);

        if (remainingEnemies == 0 && firstScreen)
        {
            isFollowing = true;
            firstScreen = false;
            transform.position = new Vector3(Mathf.Clamp(18.3f, LimitXLeft, LimitXRight), 0, -10);
        }
        if (middlePoint.x > 18.3 && remainingEnemies > 0)
        {
            isFollowing = false;
        }

        //Boss
        if (remainingBoss == 0 && bossAlive)
        {
            isFollowing = true;
            bossAlive = false;
        }
        if (middlePoint.x > 48.4 && remainingBoss > 0)
        {
            isFollowing = false;
        }

        if (isFollowing)
        {
            transform.position = new Vector3(Mathf.Clamp(middlePoint.x, LimitXLeft, LimitXRight), 0, -10);
        }
         
    }
}