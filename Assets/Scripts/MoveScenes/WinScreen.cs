using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private string newLevel;


    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            if (GameObject.FindGameObjectsWithTag("Enemies").Length == 0)
            {
                SceneManager.LoadScene("Win");
            }
        }
    }
}