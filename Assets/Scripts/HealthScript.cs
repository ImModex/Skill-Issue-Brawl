using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    private Statscript Stats;
    private int damagePerTick;
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

    public void DamageOverTime(int damageot)
    {
        damagePerTick = damageot;
        if(damagePerTick != 0)
        {
            StartCoroutine(damageOverTime());
        }
    }

    public void Stun(float stundur)
    {
        StartCoroutine(stun(stundur));
    }

    public void Velocity(float dur, float mult)
    {
        StartCoroutine(velocity(dur, mult));
    }

    public void Velocity(float mult)
    {
        Stats.moveSpeedMultiplyer *= mult;
    }

    IEnumerator stun(float stundur)
    {
        Stats.Stunned = true;
        yield return new WaitForSeconds(stundur);
        Stats.Stunned = false;
        Debug.Log("Stunned false");
    }

    IEnumerator velocity(float dur, float mult)
    {
        Debug.Log("Ms to slow");
        Stats.moveSpeedMultiplyer *= mult;
        yield return new WaitForSeconds(dur);
        Stats.moveSpeedMultiplyer /= mult;
        Debug.Log("Ms to normal");
    }

    IEnumerator damageOverTime()
    {
        while(damagePerTick != 0)
        {
            Damage(damagePerTick);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    
}
