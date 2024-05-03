using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public int damage;
    //Explosion doesnt distinguish between Friend and Foe
    void Start()
    {
        StartCoroutine(Explosiontimer());
    }
    
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Explosion");
            HealthScript HP = other.gameObject.GetComponent<HealthScript>();
            HP.Damage(damage); 
        }
       
    }

    IEnumerator Explosiontimer()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
