using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRing : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SpriteRenderer img;
    // Start is called before the first frame update
    void Start()
    {
        // make img invisible
        img.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if player is punching make visible img
        if (player.animator.GetBool("isPunching"))
        {

            img.enabled = true;
        }
        else if (!player.animator.GetBool("isPunching") && img.enabled)
        {
            img.enabled = false;
        }

    }
}
