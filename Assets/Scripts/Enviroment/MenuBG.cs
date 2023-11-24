using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > -119){
            transform.position -= new Vector3(0.005f, 0, 0);
        }
        else{
            transform.position = new Vector3(40, 0, 0);
        }
        
    }
}
