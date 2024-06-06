using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEarth : MonoBehaviour
{
    private GameObject caster;
    public GameObject projectiles;
    // Start is called before the first frame update
    void Start()
    {
        caster = transform.parent.gameObject;
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot ()
    {
        var position = caster.transform.position + new Vector3(0,1,0);
        Instantiate(projectiles, position, caster.transform.rotation, caster.transform);

        for(int i = 4; i > 0; --i)
        {
            Quaternion rotation = new Quaternion();
            float y = 30/4 * i;
            var additional = caster.transform.eulerAngles;   
            additional += new Vector3(0, y, 0);
            rotation.eulerAngles = additional;
            Instantiate(projectiles, position, rotation, caster.transform);
            additional += new Vector3(0, -2 * y, 0);
            rotation.eulerAngles = additional;
            Instantiate(projectiles, position, rotation, caster.transform);
        }
        return;
    }
}
