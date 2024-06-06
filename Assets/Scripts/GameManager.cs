using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> initialSpawns;
    public List<GameObject> respawnPoints;

    public List<GameObject> players;

    private Dictionary<GameObject, PlayerStats> playerStats = new();
    
    // Start is called before the first frame update
    void Start()
    {
        players.ForEach(player => playerStats.Add(player, new PlayerStats()));

        for (var i = 0; i < players.Count; i++)
        {
            players[i].transform.Translate(initialSpawns[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer(GameObject player)
    {
        // Regen to full hp
        player.GetComponent<HealthScript>().Regenerate();
        
        // Teleport to random respawn location
        // Esotherik, frag mich nicht wieso das sein muss.....
        var charController = player.GetComponent<CharacterController>();
        charController.enabled = false;
        player.transform.position = respawnPoints[Random.Range(0, respawnPoints.Count)].transform.position;
        charController.enabled = true;
        
        playerStats.TryGetValue(player, out var stats);

        // Falling calls this method multiple times - make sure only 1 death is added per second at max
        if (stats.lastDeath + 1 < Time.fixedTime)
        {
            stats!.deaths++;
            stats!.lastDeath = Time.fixedTime;
            Debug.Log(stats.deaths);
        }
    }

    private class PlayerStats
    {
        public int deaths = 0;
        public float lastDeath = -1;
        
        public int kills = 0;
    }
}
