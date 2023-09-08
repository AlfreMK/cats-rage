using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScenes2D : MonoBehaviour
{
    [SerializeField] private string newLevel;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public Player player;


    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            if (GameObject.FindGameObjectsWithTag("Enemies").Length == 0)
            {
                //playerStorage.initialValue = playerPosition;
                player.teleport();
                SceneManager.LoadScene(newLevel);
            }
        }
    }
}
