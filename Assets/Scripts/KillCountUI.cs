using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillCountUI : MonoBehaviour
{
	public Statscript playerStats;
	public TextMeshProUGUI killCountText;
	public Image panelImage;

	private void Start()
	{
		if (playerStats is not null)
		{
			playerStats.OnKillCountChanged += UpdateKillCountUI;
			UpdateKillCountUI(playerStats.killCount);
			SetPanelColor();
		}
	}

	private void OnDestroy()
	{
		if (playerStats is not null)
		{
			playerStats.OnKillCountChanged -= UpdateKillCountUI;
		}
	}

	public void UpdateKillCountUI(int kills)
	{
		if (killCountText is not null)
		{
			killCountText.text = kills.ToString();
		}
	}

	public void SetPanelColor()
	{
		if (playerStats is not null && panelImage is not null)
		{
			Transform wizardBodyTransform = playerStats.transform.Find("Geometry/PolyArtWizardMaskTintMat/WizardBody");

			if (wizardBodyTransform is not null)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = wizardBodyTransform.GetComponent<SkinnedMeshRenderer>();
				//Debug.Log("Wizard Body Transform: " + wizardBodyTransform);
				//Debug.Log("Skinned Mesh Renderer: " + skinnedMeshRenderer);

				if (skinnedMeshRenderer is not null)
				{
					Color materialColor = skinnedMeshRenderer.material.GetColor("_OuterChlothes");
					materialColor.a = 1f;

					//Debug.Log("Color: " + materialColor);


					panelImage.color = materialColor;
					return;
				}
			}
		}
	}
}
