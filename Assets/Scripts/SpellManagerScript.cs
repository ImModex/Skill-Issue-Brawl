using Assets.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

public class SpellManagerScript : MonoBehaviour
{
	private readonly Dictionary<(Element?, Element?), int> _spellMapping = new();

	private void Start()
	{
		ConfigureSpellMappings();
	}
	private AudioManager audioManager;
	private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

	private void Update()
	{
		// Method intentionally left empty.
	}

	public GameObject[] Spells;

	public void Cast(Element? element1, Element? element2, GameObject owner)
	{
		if (element1 is null || element2 is null)
		{
			_ = Instantiate(Spells[0], owner.transform);
			return;
		}

		int index = _spellMapping[(element1, element2)];

		playSound(index);

		_ = Instantiate(Spells[index], owner.transform);
		///_ = Instantiate(Spells[0], owner.transform); // temp
	}

	private void ConfigureSpellMappings()
	{
		_spellMapping.Add((Element.Fire, Element.Fire), 0);

		_spellMapping.Add((Element.Fire, Element.Water), 1);
		_spellMapping.Add((Element.Water, Element.Fire), 1);

		_spellMapping.Add((Element.Fire, Element.Air), 2);
		_spellMapping.Add((Element.Air, Element.Fire), 2);

		_spellMapping.Add((Element.Fire, Element.Earth), 3);
		_spellMapping.Add((Element.Earth, Element.Fire), 3);

		_spellMapping.Add((Element.Water, Element.Water), 4);

		_spellMapping.Add((Element.Water, Element.Air), 5);
		_spellMapping.Add((Element.Air, Element.Water), 5);

		_spellMapping.Add((Element.Water, Element.Earth), 6);
		_spellMapping.Add((Element.Earth, Element.Water), 6);

		_spellMapping.Add((Element.Air, Element.Air), 7);

		_spellMapping.Add((Element.Earth, Element.Air), 8);
		_spellMapping.Add((Element.Air, Element.Earth), 8);

		_spellMapping.Add((Element.Earth, Element.Earth), 9);

	}

	private void playSound(int index)
	{
		if (index == 0)
		{
			audioManager.PlaySFX(audioManager.fireball);
		}
		else if (index == 1)
		{
			audioManager.PlaySFX(audioManager.steam);
		}
		else if (index == 2)
		{
			audioManager.PlaySFX(audioManager.movespeed);
		}
		else if (index == 3)
		{
			audioManager.PlaySFX(audioManager.firewall);
		}
		else if (index == 4)
		{
			audioManager.PlaySFX(audioManager.wave);
		}
		else if (index == 5)
		{
			audioManager.PlaySFX(audioManager.stunprojectile);
		}
		else if (index == 6)
		{
			audioManager.PlaySFX(audioManager.slowfield);
		}
		else if (index == 7)
		{
			audioManager.PlaySFX(audioManager.shield);
		}
		else if (index == 8)
		{
			audioManager.PlaySFX(audioManager.tripleshot);
		}
		else if (index == 9)
		{
			audioManager.PlaySFX(audioManager.wall);
		}
	}
}