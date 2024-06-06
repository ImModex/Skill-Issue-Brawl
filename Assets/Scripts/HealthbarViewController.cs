using UnityEngine;

public class HealthbarViewController : MonoBehaviour
{
	private new Camera camera;
	public Transform target;

	private void Start()
	{
		camera = Camera.main;
	}

	private void Update()
	{
		transform.rotation = camera.transform.rotation;
	}
}
