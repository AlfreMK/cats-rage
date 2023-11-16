using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WritingEffect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    public string[] phrasesArray;
    public AudioSource audioSource;
    public string[] soundClipPaths;
    [SerializeField] float timeBetweenCharacters;
    [SerializeField] float timeBetweenWords;
    private AudioClip[] typeSounds;
    int phrasesIndex = 0;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        LoadSounds();
        EndCheck();
    }

    void LoadSounds()
    {
        typeSounds = new AudioClip[soundClipPaths.Length];
        for (int i = 0; i < soundClipPaths.Length; i++)
        {
            typeSounds[i] = Resources.Load<AudioClip>(soundClipPaths[i]);
        }
    }

    void PlayTypeSound()
    {
        if (typeSounds != null && typeSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, typeSounds.Length);
            
            if (typeSounds[randomIndex] != null)
            {
                audioSource.PlayOneShot(typeSounds[randomIndex]);
            }
        }
    }

    void EndCheck()
    {
        if (phrasesIndex <= phrasesArray.Length - 1)
        {
            _text.text = phrasesArray[phrasesIndex];
            StartCoroutine(TextVisible());
        }
    }

    private IEnumerator TextVisible()
    {
        _text.ForceMeshUpdate();
        int totalVisibleCharacters = _text.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _text.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                phrasesIndex += 1;
                Invoke("EndCheck", timeBetweenWords);
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBetweenCharacters);
            PlayTypeSound();
        }
    }
}
