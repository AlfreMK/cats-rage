using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRing : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SpriteRenderer img;
    // Start is called before the first frame update
    private string punchKey = "Punch2";
    void Start()
    {
        // make img invisible
        img.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if player is punching make visible img
        if (Input.GetAxisRaw(punchKey) != 0)
        {

            img.enabled = true;
        }
        else if (!(Input.GetAxisRaw(punchKey) != 0) && img.enabled)
        {
            img.enabled = false;
        }

    }
}
