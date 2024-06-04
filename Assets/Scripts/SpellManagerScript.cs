using StarterAssets;
using UnityEngine;

public class SpellManagerScript : MonoBehaviour
{
	private ThirdPersonController ThirdPersonController;

	// Start is called before the first frame update
	private void Start()
	{
		// Method intentionally left empty.
	}

	// Update is called once per frame
	private void Update()
	{
		// Method intentionally left empty.
	}

	public GameObject[] Spells;

	public void Cast(ThirdPersonController.Element? element1, ThirdPersonController.Element? element2, ThirdPersonController owner)
	{
		if (element1 is null || element2 is null)
		{
			_ = Instantiate(Spells[0], /*owner.GetComponentInParent<Transform>()*/ owner.transform);
			Debug.Log($"Default projectile fired");
			return;
		}

		Debug.Log($"Elements {element1} and {element2} have been selected and fired");
	}

	private void MapButtonSelectionToElements()
	{
		// Method intentionally left empty.
	}

	//    public void Cast(int type, GameObject owner)
	//    {
	//        Instantiate(Spells[type], owner.transform);

	//    }
}
