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
    public static bool isInputEnabled = true;


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

    public Player GetLeftPlayer()
    {
        if (player1.transform.position.x < player2.transform.position.x)
        {
            return player1;
        }
        else
        {
            return player2;
        }
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

    public void DisableInput()
    {
        isInputEnabled = false;
    }

    public void SetMaxX(float maxX)
    {
        mainCamera.maxX = maxX;
    }

    public bool IsCameraInMaxX()
    {
        return mainCamera.transform.position.x == mainCamera.maxX;
    }

    public void Hello()
    {
        Debug.Log("Hello");
    }


}
