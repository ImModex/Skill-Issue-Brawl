using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlingerTest : MonoBehaviour
{
    private SpellManagerScript spellManager;
    // Start is called before the first frame update
    void Start()
    {
        spellManager = GameObject.Find("SpellManager").GetComponent<SpellManagerScript>();;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            spellManager.Cast(0, transform.position, transform.rotation);
        }
    }
}
