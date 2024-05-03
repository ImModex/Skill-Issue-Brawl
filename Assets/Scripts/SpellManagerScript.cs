using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] Spells;

    public void Cast(int type, GameObject owner)
    {
        Instantiate(Spells[type], owner.transform);
        
    }
}
