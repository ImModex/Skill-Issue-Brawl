using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScript : MonoBehaviour
{
    public float moveSpeedMultiplyer;
    public float durationSec;
    private GameObject caster;
    // Start is called before the first frame update
    void Start()
    {
        caster = transform.parent.gameObject;
        HealthScript HP = caster.GetComponent<HealthScript>();
        HP.Velocity(durationSec, moveSpeedMultiplyer);
        StartCoroutine(Visual());
    }
 
    IEnumerator Visual()
    {
        yield return new WaitForSeconds(durationSec);
        Destroy(gameObject);
    }
    
}
