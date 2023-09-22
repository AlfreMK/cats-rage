using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    private float LimitXLeft = 0f;
    private float LimitXRight = 100-8.5f;
    private Player player1;
    private Player player2;
    void Start()
    {
        player1 = GameManager.Instance.GetPlayer1Script();
        player2 = GameManager.Instance.GetPlayer2Script();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player1.getIsMounted() && !player2.getIsMounted())
        {
            Vector3 middlePoint = GameManager.Instance.GetAveragePlayerPosition();
            // transform.position = new Vector3(middlePoint.x, 0, -10);
            transform.position = new Vector3(Mathf.Clamp(middlePoint.x, LimitXLeft, LimitXRight), 0, -10);
        }
        if (player1.getIsMounted())
        {
            transform.position = new Vector3(Mathf.Clamp(player2.transform.position.x, LimitXLeft, LimitXRight), 0, -10);
        }
        if (player2.getIsMounted())
        {
            transform.position = new Vector3(Mathf.Clamp(player1.transform.position.x, LimitXLeft, LimitXRight), 0, -10);
        }
    }
}
