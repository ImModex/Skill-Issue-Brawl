using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunScript : MonoBehaviour
{
    public float stundur;
    private GameObject caster;
    // Start is called before the first frame update
    void Start()
    {
        caster = gameObject.GetComponent<BasicProjectileScript>().caster;
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player")  && other.gameObject != caster)
        {
            HealthScript HP = other.gameObject.GetComponent<HealthScript>();
            HP.Stun(stundur);
        }
    }

    

}
