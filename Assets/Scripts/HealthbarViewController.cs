using UnityEngine;

public class HealthbarViewController : MonoBehaviour
{
    public Transform target;
    private Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null) cam = Camera.main;
        transform.rotation = cam.transform.rotation;
    }
}
