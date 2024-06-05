using StarterAssets;
using System.Collections.Generic;
using UnityEngine;

public class SpellManagerScript : MonoBehaviour
{
	private readonly Dictionary<(ThirdPersonController.Element?, ThirdPersonController.Element?), int> _spellMapping = new();

	private void Start()
	{
		ConfigureSpellMappings();
	}

	private void Update()
	{
		// Method intentionally left empty.
	}

	public GameObject[] Spells;

	public void Cast(ThirdPersonController.Element? element1, ThirdPersonController.Element? element2, GameObject owner)
	{
		if (element1 is null || element2 is null)
		{
			_ = Instantiate(Spells[0], owner.transform);
			return;
		}

		//_ = Instantiate(Spells[_spellMapping[(element1, element2)]], owner.transform);
		Debug.Log($"Elements {element1} and {element2} have been selected and fired");
	}

	private void ConfigureSpellMappings()
	{
		_spellMapping.Add((ThirdPersonController.Element.Fire, ThirdPersonController.Element.Fire), 0);
		_spellMapping.Add((ThirdPersonController.Element.Water, ThirdPersonController.Element.Water), 1);
		_spellMapping.Add((ThirdPersonController.Element.Earth, ThirdPersonController.Element.Earth), 2);
		_spellMapping.Add((ThirdPersonController.Element.Air, ThirdPersonController.Element.Air), 3);

		_spellMapping.Add((ThirdPersonController.Element.Fire, ThirdPersonController.Element.Water), 4);
		_spellMapping.Add((ThirdPersonController.Element.Water, ThirdPersonController.Element.Fire), 4);

		_spellMapping.Add((ThirdPersonController.Element.Fire, ThirdPersonController.Element.Earth), 5);
		_spellMapping.Add((ThirdPersonController.Element.Earth, ThirdPersonController.Element.Fire), 5);

		_spellMapping.Add((ThirdPersonController.Element.Fire, ThirdPersonController.Element.Air), 6);
		_spellMapping.Add((ThirdPersonController.Element.Air, ThirdPersonController.Element.Fire), 6);

		_spellMapping.Add((ThirdPersonController.Element.Water, ThirdPersonController.Element.Earth), 7);
		_spellMapping.Add((ThirdPersonController.Element.Earth, ThirdPersonController.Element.Water), 7);

		_spellMapping.Add((ThirdPersonController.Element.Water, ThirdPersonController.Element.Air), 8);
		_spellMapping.Add((ThirdPersonController.Element.Air, ThirdPersonController.Element.Water), 8);

		_spellMapping.Add((ThirdPersonController.Element.Earth, ThirdPersonController.Element.Air), 9);
		_spellMapping.Add((ThirdPersonController.Element.Air, ThirdPersonController.Element.Earth), 9);
	}
}
