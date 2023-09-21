using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTrigg : MonoBehaviour
{
    // Reference to the CameraFollowsPlayers script
    public CameraFollowsPlayers cameraFollowScript;

    private void Start()
    {
        cameraFollowScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollowsPlayers>();
        Debug.Log("Trigger");

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entered object has a CameraFollowsPlayers script
        if (other.CompareTag("Player1")) // You can change the tag to match your player objects
        {
            Debug.Log("Trigger");
            cameraFollowScript.StopFollowing();
        }
    }
}
