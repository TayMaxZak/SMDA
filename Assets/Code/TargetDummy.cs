using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour, Damageable
{
	[Header("Health")]
	[SerializeField]
	private float maxHealth = 10f; // Maximum health the dummy can have
	[SerializeField]
	private float curHealth; // Actual current health

	// Use this for initialization
	void Start ()
	{
		curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void TakeDamage(float dmg)
	{
		curHealth -= dmg;
		Debug.Log("Cur health = " + curHealth);

		if (curHealth <= 0)
			Destroy(gameObject);
	}
}
