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
    public AudioClip fireball;
    public AudioClip steam;
    public AudioClip movespeed;
    public AudioClip firewall;
    public AudioClip wave;
    public AudioClip stunprojectile;
    public AudioClip slowfield;
    public AudioClip shield;
    public AudioClip tripleshot;
    public AudioClip wall;

    public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    //Mit dieser Funktion k?nnen verschiedene Sound von anderen Scripts aufgerufen werden
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
