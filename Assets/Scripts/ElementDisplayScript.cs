using Assets.Scripts.Enums;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class ElementDisplay : MonoBehaviour
{
	[SerializeField]
	private int _elementDisplayIndex;

	/// <summary>
	/// Element image sprites (order: fire, water, earth, air)
	/// </summary>
	[SerializeField]
	private Sprite[] _elementImages;

	private ThirdPersonController _thirdPersonController;
	private Image _imageSlot;

	private void Start()
	{
		_thirdPersonController = transform.parent.parent.parent.GetComponent<ThirdPersonController>();
		_imageSlot = GetComponent<Image>();
	}

	private void Update()
	{
		switch (_elementDisplayIndex)
		{
			case 0:
				if (_thirdPersonController.SelectedButtons.Count < 1)
				{
					_imageSlot.sprite = null;
					return;
				}

				DisplayImage(_thirdPersonController.SelectedElements[_thirdPersonController.SelectedButtons[_elementDisplayIndex]]);
				break;

			case 1:
				if (_thirdPersonController.SelectedButtons.Count < 2)
				{
					_imageSlot.sprite = null;
					return;
				}

				DisplayImage(_thirdPersonController.SelectedElements[_thirdPersonController.SelectedButtons[_elementDisplayIndex]]);
				break;
		}
	}

	private void DisplayImage(Element element)
	{
		switch (element)
		{
			case Element.Fire: _imageSlot.sprite = _elementImages[0]; break;
			case Element.Water: _imageSlot.sprite = _elementImages[1]; break;
			case Element.Earth: _imageSlot.sprite = _elementImages[2]; break;
			case Element.Air: _imageSlot.sprite = _elementImages[3]; break;
		}
	}
}
