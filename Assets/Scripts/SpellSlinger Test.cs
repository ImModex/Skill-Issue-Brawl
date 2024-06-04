using UnityEngine;

public class SpellSlingerTest : MonoBehaviour
{
	private SpellManagerScript spellManager;

	// Start is called before the first frame update
	private void Start()
	{
		spellManager = GameObject.Find("SpellManager").GetComponent<SpellManagerScript>();
	}

	// Update is called once per frame
	private void Update()
	{
		//if (Input.GetKeyDown("1"))
		//{
		//	spellManager.Cast(0, gameObject);
		//}

		//if (Input.GetKeyDown("2"))
		//{
		//	spellManager.Cast(1, gameObject);
		//}

		//if (Input.GetKeyDown("3"))
		//{
		//	spellManager.Cast(2, gameObject);
		//}

		//if (Input.GetKeyDown("4"))
		//{
		//	spellManager.Cast(3, gameObject);
		//}

		//if (Input.GetKeyDown("5"))
		//{
		//	spellManager.Cast(4, gameObject);
		//}
	}
}
