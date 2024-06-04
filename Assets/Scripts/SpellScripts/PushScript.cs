using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushscript : MonoBehaviour
{
    public float strength;
    // Start is called before the first frame update
    void OnTriggerStay(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * strength);
    }

}
