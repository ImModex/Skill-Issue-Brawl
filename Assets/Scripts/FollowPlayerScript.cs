using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
	private GameObject player;
	[SerializeField] private Vector3 cameraOffset = new(0, 8, -9);
	[SerializeField] private float cameraRotation = 40;
	private Transform playerTransform;

	private void Start()
	{
		playerTransform = transform.parent.Find("Player");
		transform.position = playerTransform.position + cameraOffset;
		transform.rotation = Quaternion.Euler(cameraRotation, 0, 0);
	}

	private void FixedUpdate()
	{
		transform.position = playerTransform.position + cameraOffset;
		transform.rotation = Quaternion.Euler(cameraRotation, 0, 0);
	}
}
