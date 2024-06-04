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
    
    // Start is called before the first frame update
    void Start()
    {
        HealthBar.value = 1;
        UpdateHealthBarText();
        
        Debug.Log(Statscript.maxHealth);
        Debug.Log(Statscript.currentHealth);
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
        Debug.Log("I am here");
        Debug.Log(gameObject.name);
        Debug.Log(gameObject.tag);

        Statscript.currentHealth -= damage;

        HealthBar.value = ScaleHealthToHealthBar();
        UpdateHealthBarText();
    
        if (HealthBar.value <= 0)
        {
            Debug.Log("You died lmao");
        }   
        
        Debug.Log(damage + "was Taken");
    }
}
