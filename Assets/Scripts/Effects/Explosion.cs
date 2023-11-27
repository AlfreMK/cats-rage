using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    void Start()
    {
        audioSource.PlayOneShot(clip, 1f);
    }

    public void DestroyMe() { 
        Destroy(gameObject); 
    }
}
