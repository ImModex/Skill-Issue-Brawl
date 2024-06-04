using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float durationSec;
    private List <Collider> affected;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ObstacleTimer());
        transform.parent = null;
    }

    void OnTriggerStay(Collider other)
    {
        //Cant figure out how to time this
    }

    IEnumerator ObstacleTimer()
    {
        yield return new WaitForSeconds(durationSec);
        Destroy(gameObject);
    }
}
