using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffZoneScript : MonoBehaviour
{
    public float moveSpeedMultiplyerChange;
    public float range;
    public float durationSec;

    //Debuff Zone doesnt distinguish between Friend and Foe

    void Start()
    {
        transform.Translate(Vector3.forward * range) ;
        StartCoroutine(Debufftimer());
        transform.parent = null;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Statscript Stats = other.gameObject.GetComponent<Statscript>();
            AffectStats(Stats);
        }
    }


    //OnTriggerExit is not called Retarded fix but it is a common problem
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Statscript Stats = other.gameObject.GetComponent<Statscript>();
            DeAffectStats(Stats);
        }
    }

    void AffectStats(Statscript Stats)
    {
        Stats.moveSpeedMultiplyer -= moveSpeedMultiplyerChange;
    }

    void DeAffectStats(Statscript Stats)
    {
        Stats.moveSpeedMultiplyer += moveSpeedMultiplyerChange;
    }

    IEnumerator Debufftimer()
    {
        yield return new WaitForSeconds(durationSec);
        //OnTriggerExit is not called Retarded fix but it is a common problem
        transform.Translate(new Vector3(0, -10, 0));
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
