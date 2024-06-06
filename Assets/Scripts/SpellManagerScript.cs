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

		//_ = Instantiate(Spells[_spellMapping[(element1, element2)]], owner.transform);
		_ = Instantiate(Spells[0], owner.transform); // temp
	}

	private void ConfigureSpellMappings()
	{
		_spellMapping.Add((Element.Fire, Element.Fire), 0);
		_spellMapping.Add((Element.Water, Element.Water), 1);
		_spellMapping.Add((Element.Earth, Element.Earth), 2);
		_spellMapping.Add((Element.Air, Element.Air), 3);

		_spellMapping.Add((Element.Fire, Element.Water), 4);
		_spellMapping.Add((Element.Water, Element.Fire), 4);

		_spellMapping.Add((Element.Fire, Element.Earth), 5);
		_spellMapping.Add((Element.Earth, Element.Fire), 5);

		_spellMapping.Add((Element.Fire, Element.Air), 6);
		_spellMapping.Add((Element.Air, Element.Fire), 6);

		_spellMapping.Add((Element.Water, Element.Earth), 7);
		_spellMapping.Add((Element.Earth, Element.Water), 7);

		_spellMapping.Add((Element.Water, Element.Air), 8);
		_spellMapping.Add((Element.Air, Element.Water), 8);

		_spellMapping.Add((Element.Earth, Element.Air), 9);
		_spellMapping.Add((Element.Air, Element.Earth), 9);
	}
}
