using System.Collections;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
	public int damage;
	public Statscript caster;

	//Explosion doesnt distinguish between Friend and Foe
	private void Start()
	{
		_ = StartCoroutine(Explosiontimer());
	}

	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//Debug.Log("Explosion");
			HealthScript HP = other.gameObject.GetComponent<HealthScript>();
			HP.Damage(damage, caster);
		}
	}

	private IEnumerator Explosiontimer()
	{
		yield return new WaitForSeconds(0.1f);
		gameObject.GetComponent<SphereCollider>().enabled = false;
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
