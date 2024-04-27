using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public float speed;
    public float range;
    public int damage;
    public bool explosion;
    public GameObject Explosionobject;

    private Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        HealthScript HP = other.gameObject.GetComponent<HealthScript>();
        HP.Damage(damage);
        Death();
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
            /*GameObject explosionobject = gameObject.transform.GetChild(0).gameObject;
            explosionobject.SetActive(true);*/

            
            Vector3 pos = transform.position;
            Instantiate(Explosionobject, transform.position, transform.rotation);
            Debug.Log("Explosion");
            
        }
        
        
        
        Debug.Log("Dead");
        //Triggers waay to fast. Need to find other Alternative.
        Destroy(gameObject);
    }
}
