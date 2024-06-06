using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAir : MonoBehaviour
{
    private GameObject caster;
    public GameObject projectiles;
    // Start is called before the first frame update
    void Start()
    {
        caster = transform.parent.gameObject;
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Shoot ()
    {
        for(int i = 3; i > 0; --i)
        {
            Instantiate(projectiles, caster.transform);
            yield return new WaitForSeconds(0.2f);
        }
        
    }
}
