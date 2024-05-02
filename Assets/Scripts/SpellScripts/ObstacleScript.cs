using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public float durationSec;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ObstacleTimer());
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ObstacleTimer()
    {
        yield return new WaitForSeconds(durationSec);
        Destroy(gameObject);
    }
}
