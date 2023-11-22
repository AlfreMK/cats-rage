using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public Player player1;
    [SerializeField] public Player player2;
    [SerializeField] public CameraFollowsPlayers mainCamera;
    public static bool isInputEnabled = false;


    // Start is called before the first frame update
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
    }

    public CameraFollowsPlayers GetMainCamera()
    {
        return mainCamera;
    }

    public Transform GetPlayer1()
    {
        return player1.transform;
    }

    public Transform GetPlayer2()
    {
        return player2.transform;
    }

    public Player GetPlayer1Script()
    {
        return player1;
    }

    public Player GetPlayer2Script()
    {
        return player2;
    }

    public Vector2 GetAveragePlayerPosition()
    {
        return (player1.transform.position + player2.transform.position) / 2;
    }

    public bool IsInputEnabled()
    {
        return isInputEnabled;
    }

    public void EnableInput()
    {
        isInputEnabled = true;
    }

    public void Hello()
    {
        Debug.Log("Hello");
    }

}
