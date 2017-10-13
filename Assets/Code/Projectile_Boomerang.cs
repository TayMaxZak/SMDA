using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Boomerang : Projectile
{
	[SerializeField]
	private GameObject spinner;
	private Transform spinnerT;

	[SerializeField]
	private float spinSpeed = 90; // How much to rotate every second
	private Vector3 spinV;

	private float gracePeriod = 0.25f; // How long until the boomerang can be caught by the player (prevents awkward bugs)
	private bool canCatch = false;

	private float lifetime = 8f; // Will be automoatically destroyed after this time

	// Use this for initialization
	void Start ()
	{
		spinnerT = spinner.transform;
		StartCoroutine(AllowCatch());
		Destroy(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update ()
	{
		spinV = new Vector3(0, 0, spinSpeed);
		spinnerT.Rotate(spinV * Time.deltaTime);
	}

	IEnumerator AllowCatch()
	{
		yield return new WaitForSeconds(gracePeriod);
		canCatch = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			if (canCatch)
			{
				other.GetComponent<Player_Abilities>().CatchBoomerang();
				Destroy(gameObject);
			}
		}
		else
			Destroy(gameObject);
	}
}
