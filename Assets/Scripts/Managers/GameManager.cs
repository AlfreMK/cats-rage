using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;
    public Player player1;
    public Player player2;
    public Boss boss;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player1 != null && player2 != null) &&
            (player1.health <= 0 || player2.health <= 0)){
            SceneManager.LoadScene("Lose");
        }
        if (boss != null && boss.lifeBoss <= 0)
        {
            SceneManager.LoadScene("Win");
        }
    }

    public Transform GetPlayer1()
    {
        return player1.transform;
    }

    public Transform GetPlayer2()
    {
        return player2.transform;
    }
}
