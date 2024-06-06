using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Statscript Statscript;
    public Slider HealthBar;
    public TextMeshProUGUI HealthBarText;
    
    [Range(0, 100)]
    public int damagePerTick;

    public bool zoneDmg;
    public int zoneDps;
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        zoneDps = gameManager.ZoneDps;
        HealthBar.value = 1;
        UpdateHealthBarText();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            zoneDps = gameManager.ZoneDps;
        }

        if (gameManager.state == GameState.Ingame)
        {
            CheckZoneDamage();
        }
    }

    private void CheckZoneDamage()
    {
        if (DamageCircle.IsOutsideCircle_Static(transform.position))
        {
            if(!zoneDmg)
            {
                zoneDmg = true;
                damagePerTick +=  zoneDps;
                StartCoroutine(damageOverTime());
            }
 
        }
        else
        {
            if(zoneDmg)
            {
                zoneDmg = false;
                damagePerTick -= zoneDps;
                if(damagePerTick < 0)
                {
                    damagePerTick = 0; //Problem with OntriggerExit
                }
            }
            
        }
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
        if (gameManager.state != GameState.Ingame) return;
        
        Statscript.currentHealth -= damage;

        HealthBar.value = ScaleHealthToHealthBar();
        UpdateHealthBarText();
    
        if (HealthBar.value <= 0)
        {
            Debug.Log("You died lmao");
            gameManager.RespawnPlayer(gameObject);
        }   
        
        Debug.Log(damage + "was Taken");
    }

    public void ResetDOT()
    {
        damagePerTick = 0;
    }

    public void DamageOverTime(int damageot)
    {
        
        if(damagePerTick != 0)
        {
            damagePerTick += damageot;
            
        }
        else if (damageot != 0)
        {
            damagePerTick += damageot;
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
        Statscript.moveSpeedMultiplyer *= mult;
    }

    public void Regenerate()
    {
        Statscript.currentHealth = Statscript.maxHealth;

        HealthBar.value = ScaleHealthToHealthBar();
        UpdateHealthBarText();
        
        Debug.Log("Healed to full");
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
