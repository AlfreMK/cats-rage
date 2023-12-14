using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip freezeClip;
    public AudioClip breakClip;

    void Start()
    {
        audioSource.PlayOneShot(freezeClip, 1f);
    }

    public void DestroyMe()
    {
        audioSource.PlayOneShot(breakClip, 1f);
        StartCoroutine(DestroyWithDelay());
    }

    private IEnumerator DestroyWithDelay()
    {
        // Add a short delay before destroying the object
        yield return new WaitForSeconds(0.1f); 

        Destroy(gameObject);
    }
}

