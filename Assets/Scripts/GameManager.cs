using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private IEnumerator RespawnLater(GameObject player)
    {
        player.SetActive(false);
        yield return new WaitForSeconds(1);
        
        // Regen to full hp
        HealthScript HP = player.GetComponent<HealthScript>();
        HP.Regenerate();
            
            
        // Teleport to random respawn location
        TeleportPlayer(player, respawnPoints[Random.Range(0, respawnPoints.Count)].transform.position);

        HP.ResetDOT();
        player.SetActive(true);
    }
    
    public void RespawnPlayer(GameObject player)
    {
        if(!Circleshrink)
        {
            if (player.activeSelf)
            {
                StartCoroutine(RespawnLater(player));
            }
            
            playerStats.TryGetValue(player, out var stats);

            // Falling calls this method multiple times - make sure only 1 death is added per second at max
            if (stats.lastDeath + 1 < Time.fixedTime)
            {
                stats!.deaths++;
                stats!.lastDeath = Time.fixedTime;
                //Debug.Log(stats.deaths);
            }
        }else{
            if (players.FindAll(p => p.activeSelf).Count <= 1) // Last player won
            {
                StartCoroutine(nameof(LoadYourAsyncScene));
                return;
            }
            
            playerStats.TryGetValue(player, out var stats);
            // Falling calls this method multiple times - make sure only 1 death is added per second at max
            if (stats.lastDeath + 1 < Time.fixedTime)
            {
                stats!.deaths++;
                stats!.lastDeath = Time.fixedTime;
                //Debug.Log(stats.deaths);
            }

            player.SetActive(false);
            
            if (players.FindAll(p => p.activeSelf).Count <= 1) // Last player won
            {
                StartCoroutine(nameof(LoadYourAsyncScene));
                return;
            }
        }
    }
    
    private IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        foreach (var rootGameObject in SceneManager.GetSceneByBuildIndex(4).GetRootGameObjects())
        {
            if (rootGameObject.name != "WinController") continue;
            
            //var lastPlayer = players.Find(p => p.activeSelf);
            //playerStats.TryGetValue(lastPlayer, out var lastPlayerStats);

            var bestPlayer = GetBestPlayer();
            playerStats.TryGetValue(bestPlayer, out var bestPlayerStats);
            rootGameObject.GetComponent<WinController>().Display(bestPlayer.GetComponent<Statscript>().killCount.ToString(), bestPlayer.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Renderer>().material);
        }
        
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }

    private void TeleportPlayer(GameObject player, Vector3 position)
    {
        // Esotherik, frag mich nicht wieso das sein muss.....
        var charController = player.GetComponent<CharacterController>();
        charController.enabled = false;
        player.transform.position = position;
        charController.enabled = true;
    }

    private GameObject GetBestPlayer()
    {
        var sorted = playerStats.OrderBy(stats => stats.Value.kills).ThenBy(stats => stats.Value.lastDeath);
        var first = sorted.First();
        
        return first.Key;
    }
    
    private class PlayerStats
    {
        public int deaths = 0;
        public float lastDeath = -1;
        
        public int kills = 0;
    }
}
