using UnityEngine;

public class Statscript : MonoBehaviour
{
	public bool Stunned;
	//Stats of Players that can be buffed or debuffed
	public float moveSpeedMultiplyer = 1;
	public float damageMultiplyer;

	public float maxHealth = 100;
	public float currentHealth = 100;

	public int killCount = 0;

}
