using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerScore;

    public GameObject playerModel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) SceneManager.LoadScene(0);
    }

    public void Display(string score, Material material)
    {
        playerName.text = $"{material.name.Split(" ")[0]} Won!";
        playerScore.text = $"With a score of {score}";
        playerModel.GetComponent<Renderer>().material = material;
    }
}
