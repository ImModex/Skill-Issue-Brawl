using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Statscript Statscript;
    public Slider HealthBar;
    public TextMeshProUGUI HealthBarText;
    private int damagePerTick;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar.value = 1;
        UpdateHealthBarText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private float ScaleHealthToHealthBar()
    {
        return Statscript.currentHealth / Statscript.maxHealth;
    }

    private void UpdateHealthBarText()
    {
        HealthBarText.text = $"{Statscript.currentHealth} / {Statscript.maxHealth}";
    }

    public void Damage(int damage)
    {
        Statscript.currentHealth -= damage;

        HealthBar.value = ScaleHealthToHealthBar();
        UpdateHealthBarText();
    
        if (HealthBar.value <= 0)
        {
            Debug.Log("You died lmao");
        }   
        
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
        Statscript.Stunned = true;
        yield return new WaitForSeconds(stundur);
        Statscript.Stunned = false;
        Debug.Log("Stunned false");
    }

    IEnumerator velocity(float dur, float mult)
    {
        Debug.Log("Ms to slow");
        Statscript.moveSpeedMultiplyer *= mult;
        yield return new WaitForSeconds(dur);
        Statscript.moveSpeedMultiplyer /= mult;
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
