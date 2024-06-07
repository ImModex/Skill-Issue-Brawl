using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public List<Material> playerMaterials;

    private List<GameObject> players = new();
    private int playerCount = 0;

    public TextMeshProUGUI countdownText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
        GameObject newPlayer = null;
        
        foreach (var p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(players.Contains(p)) continue;
            newPlayer = p;
        }

        newPlayer!.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Renderer>().material = playerMaterials[playerCount++];
        newPlayer!.transform.GetChild(3).gameObject.SetActive(false);
        
        players.Add(newPlayer);
        DontDestroyOnLoad(newPlayer.gameObject);
        
        countdownText.gameObject.SetActive(true);

        if (countdown == 15)
        {
            InvokeRepeating(nameof(Countdown), 0, 1);
        }
    }

    private int countdown = 15;
    private void Countdown()
    {
        countdownText.text = $"Starting in {countdown--}...";

        if (countdown <= 0)
        {
            CancelInvoke();
            StartCoroutine(nameof(LoadYourAsyncScene));
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        //SceneManager.MoveGameObjectToScene(m_MyGameObject, SceneManager.GetSceneByBuildIndex(1));
        
        players.ForEach(player =>
        {
            player.transform.GetChild(3).gameObject.SetActive(true);
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("MainScene"));
        });
        
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
