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

    public void Update()
    {
        if(this.SFXSource == null)
        {
            this.SFXSource = gameObject.transform.GetChild(1).GetComponent<AudioSource>();
        }
        
    }

    //Mit dieser Funktion k?nnen verschiedene Sound von anderen Scripts aufgerufen werden
    public void PlaySFX(int index)
    {
        if (index == 0)
		{
            if (fireball == null)
            {
				return;
            }
			this.SFXSource.PlayOneShot(fireball);
		}
		else if (index == 1)
		{
			this.SFXSource.PlayOneShot(steam);
		}
		else if (index == 2)
		{
			this.SFXSource.PlayOneShot(movespeed);
		}
		else if (index == 3)
		{
			this.SFXSource.PlayOneShot(firewall);
		}
		else if (index == 4)
		{
			this.SFXSource.PlayOneShot(wave);
		}
		else if (index == 5)
		{
			this.SFXSource.PlayOneShot(stunprojectile);
		}
		else if (index == 6)
		{
			this.SFXSource.PlayOneShot(slowfield);
		}
		else if (index == 7)
		{
			this.SFXSource.PlayOneShot(shield);
		}
		else if (index == 8)
		{
			this.SFXSource.PlayOneShot(tripleshot);
		}
		else if (index == 9)
		{
			this.SFXSource.PlayOneShot(wall);
		}
        //SFXSource.PlayOneShot(clip);
    }
}
