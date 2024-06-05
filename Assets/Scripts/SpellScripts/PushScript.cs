using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushscript : MonoBehaviour
{
    public float strength;
    private GameObject caster;
    private Vector3 startpos;

    void Start()
    {
        caster = gameObject.GetComponent<BasicProjectileScript>().caster;
        startpos = gameObject.transform.position;
    }
    // Start is called before the first frame update
    void OnTriggerStay(Collider other)
    {
        
        if(other.CompareTag("Player") && other.gameObject != caster)
        {
            Vector3 dir = gameObject.transform.position - startpos;
            other.gameObject.GetComponent<ImpactRecieverScript>().AddImpact(dir, strength);
        }
        
    }

}
