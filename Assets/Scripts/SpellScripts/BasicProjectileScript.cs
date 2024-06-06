using UnityEngine;

public class BasicProjectileScript : MonoBehaviour
{
	public float speed;
	public float range;
	public int damage;
	public bool destroyOnHit;
	public bool explosion;
	public GameObject Explosionobject;
	public GameObject caster;

	private Vector3 startpos;

	// Start is called before the first frame update
	private void Start()
	{
		startpos = transform.position;
		caster = transform.parent.gameObject;
		transform.parent = null;
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(caster.name + other.gameObject.name);
		if (other.gameObject != caster)
		{
			if (other.CompareTag("Player"))
			{

				HealthScript HP = other.gameObject.GetComponent<HealthScript>();
				HP.Damage(damage, caster.GetComponent<Statscript>());

			}

			if (destroyOnHit)
			{
				Death();
			}
		}
	}

	// Update is called once per frame
	private void Update()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		if (Vector3.Distance(transform.position, startpos) >= range)
		{
			Death();
		}
	}

	private void Death()
	{
		if (explosion)
		{
			_ = transform.position;
			_ = Instantiate(Explosionobject, transform.position, transform.rotation);
		}

		Destroy(gameObject);
	}
}
