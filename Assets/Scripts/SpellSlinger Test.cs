using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlingerTest : MonoBehaviour
{
    private SpellManagerScript spellManager;
    // Start is called before the first frame update
    void Start()
    {
        spellManager = GameObject.Find("SpellManager").GetComponent<SpellManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            spellManager.Cast(0, this.gameObject);
        }

        if(Input.GetKeyDown("2"))
        {
            spellManager.Cast(1, this.gameObject);
        }

        if(Input.GetKeyDown("3"))
        {
            spellManager.Cast(2, this.gameObject);
        }

        if(Input.GetKeyDown("4"))
        {
            spellManager.Cast(3, this.gameObject);
        }

        if(Input.GetKeyDown("5"))
        {
            spellManager.Cast(4, this.gameObject);
        }
    }
}
