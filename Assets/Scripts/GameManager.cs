using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> initialSpawns;
    public List<GameObject> respawnPoints;
    public DamageCircle DamageCircle;
    public bool Circleshrink;
    public float RoundDuration;

    public int ZoneDps;

    public List<GameObject> players = new();

    private Dictionary<GameObject, PlayerStats> playerStats = new();

    public GameState state;
    
    // Start is called before the first frame update
    void Start()
    {
        StartGame(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int playerCount)
    {
        // TODO: Spawn players
        
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        players.ForEach(player => playerStats.Add(player, new PlayerStats()));

        for (var i = 0; i < players.Count; i++)
        {
            TeleportPlayer(players[i], initialSpawns[i].transform.position);
        }

        if (DamageCircle != null)
        {
            StartCoroutine(CircleShrinkTimer());
        }
    }

    IEnumerator CircleShrinkTimer()
    {
        yield return new WaitForSeconds(RoundDuration);
        Circleshrink = true;
        DamageCircle.Circleshrink = true;
    }
    
    public void RespawnPlayer(GameObject player)
    {
        if(!Circleshrink)
        {
            // Regen to full hp
            HealthScript HP = player.GetComponent<HealthScript>();
            HP.Regenerate();
            
            
            // Teleport to random respawn location
            TeleportPlayer(player, respawnPoints[Random.Range(0, respawnPoints.Count)].transform.position);

            HP.ResetDOT();
            
            playerStats.TryGetValue(player, out var stats);

            // Falling calls this method multiple times - make sure only 1 death is added per second at max
            if (stats.lastDeath + 1 < Time.fixedTime)
            {
                stats!.deaths++;
                stats!.lastDeath = Time.fixedTime;
                Debug.Log(stats.deaths);
            }
        }else{
            player.SetActive(false);
        }
        
    }

    private void TeleportPlayer(GameObject player, Vector3 position)
    {
        // Esotherik, frag mich nicht wieso das sein muss.....
        var charController = player.GetComponent<CharacterController>();
        charController.enabled = false;
        player.transform.position = position;
        charController.enabled = true;
    }
    
    private class PlayerStats
    {
        public int deaths = 0;
        public float lastDeath = -1;
        
        public int kills = 0;
    }
}
