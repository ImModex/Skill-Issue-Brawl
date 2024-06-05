using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarViewController : MonoBehaviour
{
    public Camera camera;
    public Transform target;
    public Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
        //transform.position += target.position + offset;
    }
}
