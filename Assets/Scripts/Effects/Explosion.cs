using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update

    public void DestroyMe() { 
        Destroy(gameObject); 
    }
}
