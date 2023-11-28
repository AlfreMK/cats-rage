using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUpAndDown : MonoBehaviour
{
    private float speed = 0.25f;
    private float distanceUp = 0.25f;
    private float initialX;
    private float initialY;
 
    void Start()
    {
        initialX = transform.localPosition.x;
        initialY = transform.localPosition.y;
    }

    public void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, distanceUp) + initialY;
        transform.localPosition = new Vector3(initialX, y, 0);
    }
}
