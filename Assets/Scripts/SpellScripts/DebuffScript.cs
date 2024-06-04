using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffScript : MonoBehaviour
{
    public float moveSpeedMultiplyerChange;
    public float affecteddur;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            Statscript Stats = other.gameObject.GetComponent<Statscript>();
            StartCoroutine(Hit(Stats));
        }
    }

    IEnumerator Hit (Statscript Stats)
    {
        Stats.moveSpeedMultiplyer *= moveSpeedMultiplyerChange;
        yield return new WaitForSeconds(affecteddur);
        Stats.moveSpeedMultiplyer /= moveSpeedMultiplyerChange;
    }


}
