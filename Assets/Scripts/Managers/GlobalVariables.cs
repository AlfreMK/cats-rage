using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVariables : MonoBehaviour
{
    [SerializeField]public static GlobalVariables Instance { get; private set; }
    public int globalScore = 1000;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int score)
    {
        this.globalScore += score;
        if (this.globalScore < 0)
        {
            this.globalScore = 0;
        }
    }

    public void ResetScore()
    {
        this.globalScore = 1000;
    }

    public int GetScore()
    {
        return this.globalScore;
    }

}
