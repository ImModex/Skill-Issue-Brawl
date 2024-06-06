using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Source -------------")]
    [SerializeField]  AudioSource musicSource;
    [SerializeField]  AudioSource SFXSource;

    [Header("--------- Audio Clip -------------")]
    public AudioClip background;
    public AudioClip spellshot;

    public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    //Mit dieser Funktion können verschiedene Sound von anderen Scripts aufgerufen werden
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
