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
            HealthScript HP = other.gameObject.GetComponent<HealthScript>();
            HP.Slow(affecteddur, moveSpeedMultiplyerChange);
        }
    }




}
