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

	private float gracePeriod = 0.33f; // How long until the boomerang can be caught by the player (prevents awkward bugs)
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

	/**/
	protected override void OnTriggerEnter2D(Collider2D other)
	{
		// Prevents any interaction with other projectiles
		if (other.tag != "Projectile")
		{
			// Can be caught by the player to reset boomerang cooldown
			if (other.tag == "Player")
			{
				// Only do this after a quarter of a second to avoid instantly catching what you just threw
				if (canCatch)
				{
					other.GetComponent<Player_Abilities>().CatchBoomerang();
					Destroy(gameObject);
				}
			}
			else
			{
				// Hits some unspecified object
				if (broken)
					return;
				broken = true;

				Instantiate(hitEffect, transform.position, Quaternion.identity);
				AudioUtils.PlayClipAt(hitAudio, transform.position, hitSource);
				Destroy(gameObject);
			}
		} // if projectile
	}
	/**/
}
