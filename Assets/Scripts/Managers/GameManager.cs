using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;
    private Transform player1;
    private Transform player2;
    void Start()
    {
        Instance = this;
        player1 = GameObject.FindWithTag("Player1").transform;
        player2 = GameObject.FindWithTag("Player2").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetPlayer1()
    {
        return player1;
    }

    public Transform GetPlayer2()
    {
        return player2;
    }
}
