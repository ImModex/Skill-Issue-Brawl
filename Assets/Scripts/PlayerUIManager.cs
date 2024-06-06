using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
	public GameObject playerPanelPrefab;
	public Transform panelParent;

	private void Start()
	{
		CreatePlayerPanels();
	}

	private void CreatePlayerPanels()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		int maxPanels = Mathf.Min(players.Length, 4);

		for (int i = 0; i < maxPanels; i++)
		{
			GameObject player = players[i];

			GameObject panel = Instantiate(playerPanelPrefab, panelParent);
			RectTransform rectTransform = panel.GetComponent<RectTransform>();

			float panelHeight = 75f;
			rectTransform.anchorMin = new Vector2(0, 1);
			rectTransform.anchorMax = new Vector2(0, 1);
			rectTransform.pivot = new Vector2(0, 1);
			rectTransform.anchoredPosition = new Vector2(0, -i * panelHeight);


			Statscript playerStats = player.GetComponent<Statscript>();

			if (playerStats is not null)
			{
				KillCountUI killCountUI = panel.GetComponent<KillCountUI>();

				if (killCountUI is not null)
				{
					killCountUI.playerStats = playerStats;

					killCountUI.UpdateKillCountUI(playerStats.killCount);
					//killCountUI.SetPanelColor(playerStats.playerColor);
				}
			}
		}
	}
}
