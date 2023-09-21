using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // make the wall invisible
       GetComponent<SpriteRenderer>().enabled = false;
    }
}
