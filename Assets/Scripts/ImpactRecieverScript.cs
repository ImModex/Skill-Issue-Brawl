using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactRecieverScript : MonoBehaviour
{
    public float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;
    private Statscript character;    
    void Start () {
            character = gameObject.GetComponent<Statscript>();
    }
    
    // Update is called once per frame
    void Update () {
        // apply the impact force:
        if (impact.magnitude > 0.2F)
        {
            character.MoveImpair = (impact * Time.deltaTime);
        }
        else
        {
            character.MoveImpair = Vector3.zero;
        }
            // consumes the impact energy each cycle: 
            impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
    }
    // call this function to add an impact force:
    public void AddImpact(Vector3 dir, float force){
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }
}
