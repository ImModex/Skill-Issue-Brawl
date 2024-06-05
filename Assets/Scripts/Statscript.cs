using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statscript : MonoBehaviour
{
    //Stats of Players that can be buffed or debuffed
    public float moveSpeedMultiplyer;
    public float damageMultiplyer;

    public float maxHealth = 100;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
