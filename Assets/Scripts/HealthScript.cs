using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    private Statscript Stats;
    // Start is called before the first frame update
    void Start()
    {
        Stats = gameObject.GetComponent<Statscript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage)
    {
        Debug.Log(damage + "was Taken");
    }

    public void Stun(float stundur)
    {
        StartCoroutine(stun(stundur));
    }

    public void Slow(float slowdur, float slowmult)
    {
        StartCoroutine(slow(slowdur, slowmult));
    }

    IEnumerator stun(float stundur)
    {
        Stats.Stunned = true;
        yield return new WaitForSeconds(stundur);
        Stats.Stunned = false;
        Debug.Log("Stunned false");
    }

    IEnumerator slow(float slowdur, float slowmult)
    {
        Debug.Log("Ms to slow");
        Stats.moveSpeedMultiplyer *= slowmult;
        yield return new WaitForSeconds(slowdur);
        Stats.moveSpeedMultiplyer /= slowmult;
        Debug.Log("Ms to normal");
    }
    
}
