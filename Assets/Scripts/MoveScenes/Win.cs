using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if (GameObject.FindGameObjectsWithTag("Enemies").Length == 0)
            {
                SceneManager.LoadScene("Win");
            }
        }
    }
}
