using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float durationSec;
    public int damagePerTick;
    private List <Collider> affected;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ObstacleTimer());
        transform.parent = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HealthScript HP = other.gameObject.GetComponent<HealthScript>();
            HP.DamageOverTime(damagePerTick);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HealthScript HP = other.gameObject.GetComponent<HealthScript>();
            HP.DamageOverTime(-damagePerTick);
        }
    }

    IEnumerator ObstacleTimer()
    {
        yield return new WaitForSeconds(durationSec);
        //OnTriggerExit is not called stupid fix but it is a common problem
        transform.Translate(new Vector3(0, -10, 0));
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
