using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play()
    {
    	SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame

}
