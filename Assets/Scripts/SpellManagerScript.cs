using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManagerScript : MonoBehaviour
{
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public GameObject[] Spells;

    public void Cast(int type, GameObject owner)
    {
        Instantiate(Spells[type], owner.transform);
        audioManager.PlaySFX(audioManager.spellshot);
    }
}
