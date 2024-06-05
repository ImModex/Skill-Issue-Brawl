using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMapController : MonoBehaviour
{
    public float minY = -3;

    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
            gameManager.RespawnPlayer(gameObject);
        }
    }
}
