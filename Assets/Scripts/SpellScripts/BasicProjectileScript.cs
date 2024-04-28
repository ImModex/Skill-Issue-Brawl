using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectileScript : MonoBehaviour
{
    public float speed;
    public float range;
    public int damage;
    public bool destroyOnHit;
    public bool explosion;
    public GameObject Explosionobject;
    private GameObject caster;

    private Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        caster = transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        
        if(other.CompareTag("Player"))
        {
            
            HealthScript HP = other.gameObject.GetComponent<HealthScript>();
            HP.Damage(damage);
             
        }
        if(destroyOnHit)
        {
            Death();
        } 
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, startpos) >= range)
        {
            Death();
        }
    }

    private void Death()
    {
        if(explosion)
        {
            Vector3 pos = transform.position;
            Instantiate(Explosionobject, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
