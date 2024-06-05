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

    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        HealthBar.value = 1;
        UpdateHealthBarText();
        
        InvokeRepeating(nameof(CheckZoneDamage), 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckZoneDamage()
    {
        if (DamageCircle.IsOutsideCircle_Static(transform.position))
        {
            Damage(5);
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

    public void Regenerate()
    {
        Statscript.currentHealth = Statscript.maxHealth;

        HealthBar.value = ScaleHealthToHealthBar();
        UpdateHealthBarText();
        
        Debug.Log("Healed to full");
    }
}
