using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
	private GameObject player;
	[SerializeField] private Vector3 cameraOffset = new(0, 1, -2);
	private Transform playerTransform;

	private void Start()
	{
		playerTransform = transform.parent.Find("Player");
		transform.position = playerTransform.position + cameraOffset;
		transform.rotation = Quaternion.Euler(30, 0, 0);
	}

	private void FixedUpdate()
	{
		transform.position = playerTransform.position + cameraOffset;
	}
}
