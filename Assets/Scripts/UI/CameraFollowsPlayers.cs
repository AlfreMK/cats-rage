using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    private float LimitXLeft = 0f;
    private float LimitXRight = 100-8.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 middlePoint = GameManager.Instance.GetAveragePlayerPosition();
        // transform.position = new Vector3(middlePoint.x, 0, -10);
        transform.position = new Vector3(Mathf.Clamp(middlePoint.x, LimitXLeft, LimitXRight), 0, -10);
    }
}
